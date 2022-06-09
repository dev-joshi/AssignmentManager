namespace AssignmentManager.Queue.Business.DI
{
    using AssignmentManager.Auth.Business.DI;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// DI extentions for adding queue business.
    /// </summary>
    public static class QueueBusinessServiceCollectionExtention
    {
        /// <summary>
        /// Adds the queue business to service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>service collection.</returns>
        public static IServiceCollection AddQueueBusiness(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IQueueEventAggregator, QueueEventAggregator>();
            serviceCollection.AddTokenGeneration();
            return serviceCollection;
        }
    }
}
