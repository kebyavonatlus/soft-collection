using SoftCollection.SharedKernel.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<IBusinessClock>(sp => new BusinessClock(sp.GetRequiredService<TimeProvider>()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health");

var api = app.MapGroup("/api");

// Опер. день и время организации — показываются внизу бокового меню (ТЗ, раздел 5).
api.MapGet("/system/info", (IBusinessClock clock) => new
{
    Product = "Soft Collection",
    Version = typeof(Program).Assembly.GetName().Version?.ToString(),
    TimeZone = clock.TimeZone.Id,
    Today = clock.Today,
})
.WithName("GetSystemInfo");

await app.RunAsync();

public partial class Program;
