namespace AssignmentManager.Entities
{
    /// <summary>
    /// Holds details for authorization filter.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Gets or sets the Role to allow.
        /// </summary>
        public Roles Role { get; set; }

        /// <summary>
        /// Gets or sets the User id to allow.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Service id to allow.
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the UserName to allow.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the ServiceName to allow.
        /// </summary>
        public string ServiceName { get; set; }
    }
}
