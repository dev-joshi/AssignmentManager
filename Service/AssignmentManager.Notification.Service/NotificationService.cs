namespace AssignmentManager.Queue.Service
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AssignmentManager.DB.Storage;
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
        /// The database setup.
        /// </summary>
        private readonly IDatabaseSetup databaseSetup;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<NotificationService> logger;

        /// <summary>
        /// The application lifetime.
        /// </summary>
        private readonly IHostApplicationLifetime applicationLifetime;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="databaseSetup">The database setup.</param>
        /// <param name="applicationLifetime">The application lifetime.</param>
        /// <param name="logger">The logger.</param>
        public NotificationService(
            IQueueEventAggregator eventAggregator,
            IDatabaseSetup databaseSetup,
            IHostApplicationLifetime applicationLifetime,
            ILogger<NotificationService> logger)
        {
            this.eventAggregator = eventAggregator;
            this.databaseSetup = databaseSetup;
            this.applicationLifetime = applicationLifetime;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                this.databaseSetup.SetupDatabase();
                this.databaseSetup.SeedData();
                await this.eventAggregator.SubscribeAsync<AssignmentCreatedMessage>(x =>
                {
                    this.logger.LogInformation("Got event for Assignment Creted with ID : {id}, Name : {name}", x.Id, x.Name);
                    return Task.CompletedTask;
                });
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
            this.eventAggregator.Dispose();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Waits for database.
        /// </summary>
        /// <exception cref="System.TimeoutException">Timed out while waiting to connect to DB.</exception>
        private async Task WaitForDB()
        {
            // wait for DB with 5 attempts 20 seconds apart.
            var attemptsLeft = 5;

            while (attemptsLeft > 0)
            {
                this.logger.LogInformation("waiting for DB, attempt count : {attemptsLeft}", 6 - attemptsLeft);
                if (this.databaseSetup.CanConnect())
                {
                    this.logger.LogInformation("DB Connected");
                    break;
                }

                attemptsLeft--;
                await Task.Delay(TimeSpan.FromSeconds(20)).ConfigureAwait(false);
            }

            if (attemptsLeft < 0)
            {
                throw new TimeoutException("Timed out while waiting to connect to DB");
            }
        }
    }
}
