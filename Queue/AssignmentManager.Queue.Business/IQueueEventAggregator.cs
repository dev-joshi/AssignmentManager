namespace AssignmentManager.Queue.Business
{
    using AssignmentManager.Entities.QueueMessage;

    /// <summary>
    /// Event Aggregator to Publish and Subscribe message on Queue.
    /// </summary>
    public interface IQueueEventAggregator : IDisposable
    {
        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task PublishAsync<T>(T message)
            where T : BaseMessage;

        /// <summary>
        /// Subscribes to the specified message with a callback.
        /// </summary>
        /// <typeparam name="T">Type of message.</typeparam>
        /// <param name="callback">The callback.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task SubscribeAsync<T>(Func<T, Task> callback)
            where T : BaseMessage;
    }
}