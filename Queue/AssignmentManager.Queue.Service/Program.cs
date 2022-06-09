namespace AssignmentManager.Queue.Service
{
    using AssignmentManager.Auth.Business.DI;
    using AssignmentManager.Common.Logging;
    using AssignmentManager.DB.EF.DI;
    using AssignmentManager.Queue.Service.MQTT;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    ///  Program.
    /// </summary>
    public class Program
    {
        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        /// <summary>Creates the host builder.</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Host Builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogger();
                    services.AddDatabase();
                    services.AddTokenValidation();
                    services.TryAddSingleton<IMqttBroker, MqttBroker>();
                    services.AddHostedService<QueueService>();
                });
    }
}