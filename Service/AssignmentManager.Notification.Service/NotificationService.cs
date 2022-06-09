namespace AssignmentManager.Queue.Service
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AssignmentManager.Entities.QueueMessage;
    using AssignmentManager.Queue.Business;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The Queue Service.
    /// </summary>
    public class NotificationService : IHostedService
    {
        /// <summary>
        /// The event aggregator.
        /// </summary>
        private readonly IQueueEventAggregator eventAggregator;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<NotificationService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="logger">The logger.</param>
        public NotificationService(
            IQueueEventAggregator eventAggregator,
            ILogger<NotificationService> logger)
        {
            this.eventAggregator = eventAggregator;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await this.eventAggregator.SubscribeAsync<AssignmentCreatedMessage>(x =>
                {
                    this.logger.LogInformation("Got event for Assignment Creted with ID : {id}, Name : {name}", x.Id, x.Name);
                    return Task.CompletedTask;
                });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while starting Queue Service");
            }
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.eventAggregator.Dispose();
            return Task.CompletedTask;
        }
    }
}
