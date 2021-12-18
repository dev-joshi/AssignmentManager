namespace AssignmentManager.Common.Logging
{
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    /// <summary>
    /// Extention Method to Add logging to DI.
    /// </summary>
    public static class LoggingServiceCollectionExtention
    {
        /// <summary>
        /// Adds the logging.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>Service Collection.</returns>
        public static IServiceCollection AddLogger(this IServiceCollection serviceCollection)
        {
            // Configure Serilog.
            Log.Logger = GetLogger();

            // Add Serilog to DI.
            serviceCollection.AddLogging(
                loggingBuilder => 
                loggingBuilder.AddSerilog(dispose: true));

            return serviceCollection;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <returns>the logger.</returns>
        private static Serilog.ILogger GetLogger()
        {
            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {MachineName} {ProcessName} ({ThreadId}) [{Level:u3}] {Message:l} {Exception}{NewLine}";

            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithMachineName()
                .Enrich.WithProcessName()
                .WriteTo.Console(outputTemplate: outputTemplate)
                .CreateLogger();
        }
    }
}
