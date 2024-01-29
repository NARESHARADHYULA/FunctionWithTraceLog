using FunctionWithTraceLog.Bl;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunctionWithTraceLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWebApplication((worker) =>
                {
                    worker.Services.ConfigureFunctionsApplicationInsights();
                })
                .ConfigureAppConfiguration((HostBuilderContext hostContext, IConfigurationBuilder builder) =>
                {
                    // Add custom configuration sources
                    builder.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices((HostBuilderContext hostContext, IServiceCollection services) =>
                {
                    // register DI
                    // Add ApplicationInsights services for non-HTTP applications.
                    // See https://learn.microsoft.com/en-us/azure/azure-monitor/app/worker-service and
                    // See https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide#application-insights
                    services.AddApplicationInsightsTelemetryWorkerService();

                    // Add function app specific ApplicationInsights services.
                    // See https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide#application-insights
                    services.ConfigureFunctionsApplicationInsights();

                    // You will need extra configuration because above will only log per default Warning (default AI configuration) and above because of following line:
                    // https://github.com/microsoft/ApplicationInsights-dotnet/blob/main/NETCORE/src/Shared/Extensions/ApplicationInsightsExtensions.cs#L427
                    // This is documented here:
                    // https://github.com/microsoft/ApplicationInsights-dotnet/issues/2610#issuecomment-1316672650
                    // So remove the default logger rule (warning and above). This will result that the default will be Information.
                    services.Configure<LoggerFilterOptions>(options =>
                    {
                        var toRemove = options.Rules.FirstOrDefault(rule => rule.ProviderName
                            == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

                        if (toRemove is not null)
                        {
                            options.Rules.Remove(toRemove);
                        }
                    });

                    services
                    .AddScoped<IBusinessLogic>(c => new BusinessLogic(
                       c.GetRequiredService<ILogger<BusinessLogic>>()));

                })
                  .ConfigureLogging((hostingContext, logging) =>
                  {
                      // Make sure the configuration of the appsettings.json file is picked up.
                      logging.AddConfiguration(hostingContext.Configuration.GetSection("WorkerLogging"));
                  })
                .Build();

            host.Run();
        }
    }
}
