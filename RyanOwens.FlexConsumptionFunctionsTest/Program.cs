using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddOpenTelemetry()
            .UseFunctionsWorkerDefaults()
            .UseAzureMonitor();
    })
    .Build();

host.Run();