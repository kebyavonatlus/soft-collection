var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

// Публичный контур (DMZ): только страница должника (Э-13) и API мобильного приложения.
// Прямого доступа к БД отсюда нет — только узкий API ядра.
app.MapHealthChecks("/health");

await app.RunAsync();
