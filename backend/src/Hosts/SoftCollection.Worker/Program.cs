var builder = Host.CreateApplicationBuilder(args);

// Здесь будут фоновые задачи (Hangfire): обмен с АБС, ночной пересчёт,
// outbox в АБС, отправка SMS, формирование отчётов.

var host = builder.Build();
await host.RunAsync();
