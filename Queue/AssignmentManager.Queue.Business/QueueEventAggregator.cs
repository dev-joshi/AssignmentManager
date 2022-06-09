namespace AssignmentManager.Queue.Business
{
    using System.Collections.Concurrent;
    using System.Text;
    using AssignmentManager.Auth.Business;
    using AssignmentManager.Entities;
    using AssignmentManager.Entities.QueueMessage;
    using Microsoft.Extensions.Logging;
    using MQTTnet;
    using MQTTnet.Client;
    using Newtonsoft.Json;

    /// <inheritdoc />
    internal class QueueEventAggregator : IQueueEventAggregator
    {
        /// <summary>
        /// The token generator.
        /// </summary>
        private readonly ITokenGenerator tokenGenerator;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<QueueEventAggregator> logger;

        /// <summary>
        /// The client.
        /// </summary>
        private IMqttClient client;

        /// <summary>
        /// The subscriptions.
        /// </summary>
        private ConcurrentDictionary<string, List<Func<BaseMessage, Task>>> subscriptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueEventAggregator"/> class.
        /// </summary>
        /// <param name="tokenGenerator">The token generator.</param>
        /// <param name="logger">The logger.</param>
        public QueueEventAggregator(
            ITokenGenerator tokenGenerator,
            ILogger<QueueEventAggregator> logger)
        {
            this.tokenGenerator = tokenGenerator;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task PublishAsync<T>(T message)
            where T : BaseMessage
        {
            await this.CheckConnectionAsync();

            var applicationMessage = new MqttApplicationMessageBuilder()
                .WithTopic(typeof(T).Name)
                .WithPayload(JsonConvert.SerializeObject(message))
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                .Build();

            var result = await this.client.PublishAsync(applicationMessage);

            if (result == null
                || result.ReasonCode != MqttClientPublishReasonCode.Success)
            {
                this.logger.LogError("Failed to publish message with code : {code}, reason : {reason}", result?.ReasonCode, result?.ReasonString);
            }
            else
            {
                this.logger.LogDebug("Message type {type} published", typeof(T));
            }
        }

        /// <inheritdoc />
        public async Task SubscribeAsync<T>(Func<T, Task> callback)
            where T : BaseMessage
        {
            await this.CheckConnectionAsync();

            var name = typeof(T).Name;
            var call = (Func<BaseMessage, Task>)callback;

            var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                    .WithTopicFilter(name, MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                    .Build();

            if (this.subscriptions.ContainsKey(name))
            {
                if (this.subscriptions.TryGetValue(name, out var list))
                {
                    list.Add(call);
                }
            }
            else
            {
                this.subscriptions.TryAdd(name, new List<Func<BaseMessage, Task>> { call });
            }

            await this.client.SubscribeAsync(subscribeOptions);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.client?.IsConnected == true)
            {
                this.client.ApplicationMessageReceivedAsync -= this.HandleMessageAsync;
                this.client.DisconnectAsync().ConfigureAwait(false)
                    .GetAwaiter().GetResult();
            }

            this.client?.Dispose();
        }

        /// <summary>
        /// Connects this instance to MQTT Queue.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task CheckConnectionAsync()
        {
            if (this.client == null
                || !this.client.IsConnected)
            {
                this.client = new MqttFactory().CreateMqttClient();

                var connectResult = await this.client.ConnectAsync(
                new MqttClientOptionsBuilder()
                    .WithTcpServer(ConfigurationConstants.HostName, ConfigurationConstants.MqttPort)
                    .WithClientId(Guid.NewGuid().ToString())
                    .WithUserProperty(ClaimConstants.ServiceName, "Test Service 1")
                    .WithCredentials("Test Service 1", (await this.tokenGenerator.GenerateTokenForServiceAsync(1)).AccessToken)
                    .WithCleanSession(true)
                    .Build());

                if (connectResult == null
                    || connectResult.ResultCode != MqttClientConnectResultCode.Success)
                {
                    this.logger.LogError("Failed to connect to Queue with error code : {code}, reason : {reason}", connectResult?.ResultCode, connectResult?.ReasonString);
                }
                else
                {
                    this.logger.LogDebug("MQTT Client Connected");
                    this.client.ApplicationMessageReceivedAsync += this.HandleMessageAsync;
                }
            }
        }

        /// <summary>
        /// Handles any message on queue.
        /// </summary>
        /// <param name="messageEvent">The <see cref="MqttApplicationMessageReceivedEventArgs"/> instance containing the event data.</param>
        private async Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs messageEvent)
        {
            var name = messageEvent?.ApplicationMessage?.Topic;

            if (!string.IsNullOrWhiteSpace(name))
            {
                if (this.subscriptions.TryGetValue(name, out var funcList))
                {
                    var type = Type.GetType(name);
                    var messageObject = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(messageEvent.ApplicationMessage.Payload), type);

                    if (messageObject != null)
                    {
                        if (messageObject is BaseMessage message)
                        {
                            await Task.WhenAll(funcList.Select(async x =>
                            {
                                try
                                {
                                    await x.Invoke(message).ConfigureAwait(false);
                                }
                                catch (Exception ex)
                                {
                                    this.logger.LogError(ex, "Failed to run a queue message callback");
                                }
                            }));
                        }
                    }
                }
            }
        }
    }
}
