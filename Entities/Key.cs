namespace AssignmentManager.Entities
{
    using System;

    /// <summary>
    /// Entity to store Secret Key.
    /// </summary>
    public class Key
    {
        /// <summary>
        /// Gets or Sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the created on time.
        /// </summary>
        /// <value>
        /// The created on time.
        /// </value>
        public DateTime CreatedOn { get; set; }
    }
}
