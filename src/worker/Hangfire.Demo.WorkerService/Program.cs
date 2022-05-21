using Hangfire.PostgreSql;

namespace Hangfire.Demo.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext,services) =>
                { 
                    services.AddHostedService<Worker>();
                    services.AddHangfire(hangfireGlobalConfiguration =>
                    {
                        string storageConnectionString = hostContext.Configuration["Hangfire:Database:ConnectionString"];
                        hangfireGlobalConfiguration
                            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                            .UseSimpleAssemblyNameTypeSerializer()
                            .UseRecommendedSerializerSettings()
                            .UsePostgreSqlStorage(storageConnectionString);
                    });
                    services.AddHangfireServer(serverOptions =>
                    {
                        serverOptions.ServerName = hostContext.Configuration["ServerName"];
                        serverOptions.Queues = new[] { hostContext.Configuration["QueueName"] };
                    });
                })
                .Build();

            host.Run();
        }
    }
}