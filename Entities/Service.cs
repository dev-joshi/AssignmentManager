namespace AssignmentManager.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Service Role.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Gets or Sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the Service name.
        /// </summary>
        /// <value>
        /// The Service name.
        /// </value>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the the roles..
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public ICollection<Role> Roles { get; set; }

    }
}
