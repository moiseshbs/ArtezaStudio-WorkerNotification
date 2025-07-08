using ArtezaStudio.WorkerNotification.Consumers;
using ArtezaStudio.WorkerNotification.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();
        services.AddSingleton<ApiService>();
        services.AddSingleton<EmailService>();
        services.AddHostedService<CurtidaCreatedConsumer>();
    })
    .Build()
    .Run();
