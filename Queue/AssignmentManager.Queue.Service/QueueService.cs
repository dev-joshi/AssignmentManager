namespace AssignmentManager.Queue.Service
{
    using System.Threading;
    using System.Threading.Tasks;
    using AssignmentManager.Entities;
    using AssignmentManager.Queue.Service.MQTT;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Queue Service.
    /// </summary>
    public class QueueService : IHostedService
    {
        /// <summary>
        /// The MQTT broker.
        /// </summary>
        private readonly IMqttBroker mqttBroker;

        /// <summary>
        /// The application lifetime.
        /// </summary>
        private readonly IHostApplicationLifetime applicationLifetime;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<QueueService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueService"/> class.
        /// </summary>
        /// <param name="mqttBroker">The MQTT broker.</param>
        /// <param name="applicationLifetime">The application lifetime.</param>
        /// <param name="logger">The logger.</param>
        public QueueService(
            IMqttBroker mqttBroker,
            IHostApplicationLifetime applicationLifetime,
            ILogger<QueueService> logger)
        {
            this.mqttBroker = mqttBroker;
            this.applicationLifetime = applicationLifetime;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await this.mqttBroker.StartAsync(ConfigurationConstants.MqttPort).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while starting Queue Service");
                this.applicationLifetime.StopApplication();
            }
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.mqttBroker.Dispose();
            return Task.CompletedTask;
        }
    }
}
