namespace AssignmentManager.Queue.Service.MQTT
{
    using System.Threading.Tasks;

    /// <summary>
    /// MQTT Message Broker.
    /// </summary>
    public interface IMqttBroker : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is started; otherwise, <c>false</c>.
        /// </value>
        bool IsStarted { get; }

        /// <summary>
        /// Starts the MQTT Broker instance.
        /// </summary>
        /// <param name="port">port number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task StartAsync(int port);
    }
}