// Заводит бэклог Soft Collection в GitHub: метки, вехи, issues и доску GitHub Projects.
//
// Требуется: .NET 10 SDK и GitHub CLI (gh), авторизованный с правом на проекты:
//   gh auth login
//   gh auth refresh -s project
//
// Запуск из корня репозитория:
//   dotnet run scripts/BootstrapGithub.cs                 — всё
//   dotnet run scripts/BootstrapGithub.cs -- --dry-run    — только показать, что будет сделано
//   dotnet run scripts/BootstrapGithub.cs -- --no-project — без доски Projects
//   dotnet run scripts/BootstrapGithub.cs -- --repo owner/name
//
// Скрипт идемпотентный: повторный запуск не создаёт дублей (issues сверяются по заголовку).

using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

var dryRun = args.Contains("--dry-run");
var withProject = !args.Contains("--no-project");
var repoArgIndex = Array.IndexOf(args, "--repo");
var backlogPath = Path.Combine(AppContext.GetData("EntryPointFileDirectoryPath") as string ?? "scripts", "backlog.json");

var backlog = JsonNode.Parse(File.ReadAllText(backlogPath))!.AsObject();
var repo = repoArgIndex >= 0 ? args[repoArgIndex + 1] : Gh("repo", "view", "--json", "nameWithOwner", "-q", ".nameWithOwner").Trim();
var owner = repo.Split('/')[0];
var projectTitle = backlog["project"]!.GetValue<string>();

Console.WriteLine($"Репозиторий: {repo}{(dryRun ? "  (dry-run — ничего не меняется)" : "")}");

// 1. Метки
Console.WriteLine("\n== Метки");
foreach (var label in backlog["labels"]!.AsArray())
{
    var name = label!["name"]!.GetValue<string>();
    Run("label", "create", name, "--repo", repo, "--color", label["color"]!.GetValue<string>(),
        "--description", label["description"]!.GetValue<string>(), "--force");
    Console.WriteLine($"  {name}");
}

// 2. Вехи
Console.WriteLine("\n== Вехи");
var existingMilestones = JsonNode.Parse(Gh("api", $"repos/{repo}/milestones?state=all&per_page=100"))!.AsArray()
    .Select(m => m!["title"]!.GetValue<string>())
    .ToHashSet();
foreach (var milestone in backlog["milestones"]!.AsArray())
{
    var title = milestone!["title"]!.GetValue<string>();
    if (existingMilestones.Contains(title))
    {
        Console.WriteLine($"  = {title}");
        continue;
    }

    Run("api", "-X", "POST", $"repos/{repo}/milestones", "-f", $"title={title}",
        "-f", $"description={milestone["description"]!.GetValue<string>()}");
    Console.WriteLine($"  + {title}");
}

// 3. Issues
Console.WriteLine("\n== Задачи");
var existingIssues = JsonNode.Parse(Gh("issue", "list", "--repo", repo, "--state", "all", "--limit", "1000", "--json", "title,url"))!.AsArray()
    .ToDictionary(i => i!["title"]!.GetValue<string>(), i => i!["url"]!.GetValue<string>());

var created = new List<(string Url, string Milestone, bool Closed)>();
foreach (var issue in backlog["issues"]!.AsArray())
{
    var title = issue!["title"]!.GetValue<string>();
    var milestone = issue["milestone"]!.GetValue<string>();
    var closed = issue["closed"]!.GetValue<bool>();

    if (existingIssues.TryGetValue(title, out var existingUrl))
    {
        Console.WriteLine($"  = {title}");
        created.Add((existingUrl, milestone, closed));
        continue;
    }

    var bodyFile = Path.GetTempFileName();
    File.WriteAllText(bodyFile, issue["body"]!.GetValue<string>());
    var createArgs = new List<string> { "issue", "create", "--repo", repo, "--title", title, "--body-file", bodyFile, "--milestone", milestone };
    foreach (var label in issue["labels"]!.AsArray())
    {
        createArgs.Add("--label");
        createArgs.Add(label!.GetValue<string>());
    }

    var url = dryRun ? $"https://github.com/{repo}/issues/(new)" : Gh([.. createArgs]).Trim().Split('\n').Last();
    if (dryRun)
    {
        Console.WriteLine($"  [dry-run] gh {string.Join(' ', createArgs.Take(6))} …");
    }

    File.Delete(bodyFile);
    if (closed)
    {
        Run("issue", "close", url, "--repo", repo, "--reason", "completed");
    }

    Console.WriteLine($"  + {title}{(closed ? "  (закрыта)" : "")}");
    created.Add((url, milestone, closed));
}

// 4. Доска GitHub Projects
if (withProject && !dryRun)
{
    Console.WriteLine("\n== Доска GitHub Projects");
    var projects = JsonNode.Parse(Gh("project", "list", "--owner", owner, "--format", "json", "--limit", "100"))!["projects"]!.AsArray();
    var project = projects.FirstOrDefault(p => p!["title"]!.GetValue<string>() == projectTitle)
        ?? JsonNode.Parse(Gh("project", "create", "--owner", owner, "--title", projectTitle, "--format", "json"))!;
    var projectNumber = project["number"]!.GetValue<int>().ToString(System.Globalization.CultureInfo.InvariantCulture);
    var projectId = project["id"]!.GetValue<string>();
    Console.WriteLine($"  Проект: {project["url"]?.GetValue<string>() ?? projectTitle}");

    TryGh("project", "link", projectNumber, "--owner", owner, "--repo", repo);

    var fields = JsonNode.Parse(Gh("project", "field-list", projectNumber, "--owner", owner, "--format", "json"))!["fields"]!.AsArray();
    var status = fields.FirstOrDefault(f => f!["name"]!.GetValue<string>() == "Status");
    string? OptionId(string name) => status?["options"]?.AsArray()
        .FirstOrDefault(o => o!["name"]!.GetValue<string>() == name)?["id"]?.GetValue<string>();

    // Текущая работа — в «Todo», сделанное — в «Done», остальное остаётся без статуса (бэклог).
    var currentMilestones = new HashSet<string> { "Этап 0 · Подготовка", "S0 · Каркас" };
    foreach (var (url, milestone, closed) in created)
    {
        var item = JsonNode.Parse(Gh("project", "item-add", projectNumber, "--owner", owner, "--url", url, "--format", "json"))!;
        var optionId = closed ? OptionId("Done") : currentMilestones.Contains(milestone) ? OptionId("Todo") : null;
        if (status is not null && optionId is not null)
        {
            Gh("project", "item-edit", "--id", item["id"]!.GetValue<string>(), "--project-id", projectId,
                "--field-id", status["id"]!.GetValue<string>(), "--single-select-option-id", optionId);
        }

        Console.Write('.');
    }

    Console.WriteLine($"\n  Добавлено на доску: {created.Count}");
    Console.WriteLine("  Совет: в настройках вида Board включите группировку по Milestone — получится доска по спринтам.");
}

Console.WriteLine("\nГотово.");

void Run(params string[] ghArgs)
{
    if (dryRun)
    {
        return;
    }

    Gh(ghArgs);
}

void TryGh(params string[] ghArgs)
{
    try
    {
        Gh(ghArgs);
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine($"  (пропущено: {e.Message.Split('\n')[0]})");
    }
}

static string Gh(params string[] ghArgs)
{
    var psi = new ProcessStartInfo("gh")
    {
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        StandardOutputEncoding = System.Text.Encoding.UTF8,
    };
    foreach (var a in ghArgs)
    {
        psi.ArgumentList.Add(a);
    }

    using var p = Process.Start(psi) ?? throw new InvalidOperationException("Не удалось запустить gh. Установите GitHub CLI.");
    var stdout = p.StandardOutput.ReadToEndAsync();
    var stderr = p.StandardError.ReadToEndAsync();
    p.WaitForExit();
    if (p.ExitCode != 0)
    {
        throw new InvalidOperationException($"gh {string.Join(' ', ghArgs.Take(3))}: {stderr.Result.Trim()}");
    }

    return stdout.Result;
}
