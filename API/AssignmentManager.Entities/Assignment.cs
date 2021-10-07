namespace AssignmentManager.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Assignment.
    /// </summary>
    public class Assignment
    {
        /// <summary>
        /// Gets or Sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or Sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public virtual ICollection<Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets or Sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or Sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Gets or Sets the deadline.
        /// </summary>
        /// <value>
        /// The deadline.
        /// </value>
        public DateTime Deadline { get; set; }
    }
}
