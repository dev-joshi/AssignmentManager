namespace AssignmentManager.Entities.ApiModel.Request
{
    /// <summary>
    /// The Service Credentials.
    /// </summary>
    public class ServiceCreds
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the service secret.
        /// </summary>
        /// <value>
        /// The service secret.
        /// </value>
        public string ServiceSecret { get; set; }
    }
}
