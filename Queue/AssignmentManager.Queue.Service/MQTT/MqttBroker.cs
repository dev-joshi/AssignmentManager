namespace AssignmentManager.Queue.Service.MQTT
{
    using AssignmentManager.Auth.Business;
    using AssignmentManager.Entities;
    using Microsoft.Extensions.Logging;
    using MQTTnet;
    using MQTTnet.Protocol;
    using MQTTnet.Server;

    /// <inheritdoc />
    public class MqttBroker : IMqttBroker
    {
        /// <summary>
        /// The token validator.
        /// </summary>
        private readonly ITokenValidator tokenValidator;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<MqttBroker> logger;

        /// <summary>
        /// The server.
        /// </summary>
        private MqttServer server;

        /// <summary>
        /// Initializes a new instance of the <see cref="MqttBroker"/> class.
        /// </summary>
        /// <param name="tokenValidator">The token validator.</param>
        /// <param name="logger">The logger.</param>
        public MqttBroker(
            ITokenValidator tokenValidator,
            ILogger<MqttBroker> logger)
        {
            this.tokenValidator = tokenValidator;
            this.logger = logger;
        }

        /// <inheritdoc />
        public bool IsStarted => this.server.IsStarted;

        /// <inheritdoc />
        public async Task StartAsync(int port)
        {
            this.logger.LogInformation("Starting MQTT server on {host}:{port}", System.Net.IPAddress.Loopback, port);

            this.server = new MqttFactory().CreateMqttServer(new MqttServerOptionsBuilder()
                .WithDefaultEndpointBoundIPAddress(System.Net.IPAddress.Loopback)
                .WithDefaultEndpointPort(port)
                .WithDefaultCommunicationTimeout(TimeSpan.FromSeconds(30))
                .Build());

            this.server.ValidatingConnectionAsync += this.Validate;
            this.server.ClientConnectedAsync += this.HandleEvent;
            this.server.ClientDisconnectedAsync += this.HandleEvent;
            this.server.ClientSubscribedTopicAsync += this.HandleEvent;
            this.server.ClientUnsubscribedTopicAsync += this.HandleEvent;
            this.server.InterceptingPublishAsync += this.HandleEvent;

            await this.server.StartAsync().ConfigureAwait(false);

            this.logger.LogInformation("Started MQTT server");
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.server?.IsStarted == true)
            {
                this.logger.LogInformation("Stopping MQTT server");
                this.server?.StopAsync().ConfigureAwait(false)
                    .GetAwaiter().GetResult();
            }

            this.server?.Dispose();
            this.logger.LogInformation("Stopped MQTT server");
        }

        /// <summary>
        /// Validates the specified connection event.
        /// </summary>
        /// <param name="e">The <see cref="ValidatingConnectionEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task Validate(ValidatingConnectionEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Password))
            {
                this.logger.LogWarning("Rejecting MQTT connection with empty password");
                e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
            }

            if (e.UserProperties != null)
            {
                var serviceName = e.UserProperties.FirstOrDefault(p => p.Name == ClaimConstants.ServiceName);

                if (serviceName == null
                    || string.IsNullOrWhiteSpace(serviceName.Value)
                    || !this.tokenValidator.TryValidateForServiceRole(e.Password, serviceName.Value, Roles.ConnectToMessageQueue))
                {
                    this.logger.LogWarning("Rejecting MQTT connection with incorrect service role");
                    e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
                }

                this.logger.LogDebug("Accepted MQTT Connection from service {servicename}", serviceName.Value);
            }
            else
            {
                this.logger.LogWarning("Rejecting MQTT connection without user properties");
                e.ReasonCode = MqttConnectReasonCode.ClientIdentifierNotValid;
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Handles the client or message event.
        /// </summary>
        /// <param name="eArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task HandleEvent(EventArgs eArgs)
        {
            switch (eArgs)
            {
                case ClientConnectedEventArgs cc:
                    this.logger.LogInformation("Client Connected : {id}", cc?.ClientId);
                    break;
                case ClientDisconnectedEventArgs cd:
                    this.logger.LogInformation("Client Disconnected : {id}", cd?.ClientId);
                    break;
                case ClientSubscribedTopicEventArgs cs:
                    this.logger.LogInformation("Client {id} Subscribed to topic {topic}", cs?.ClientId, cs?.TopicFilter?.Topic);
                    break;
                case ClientUnsubscribedTopicEventArgs cu:
                    this.logger.LogInformation("Client {id} Unsubscribed from topic {topic}", cu?.ClientId, cu?.TopicFilter);
                    break;
                case InterceptingPublishEventArgs ip:
                    this.logger.LogDebug("Event Published on Topic : {message}", ip.ApplicationMessage.Topic);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
