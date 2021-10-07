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
        /// Initializes a new instance of the <see cref="Assignment"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="attachments">The attachments.</param>
        /// <param name="createdOn">The created on.</param>
        /// <param name="modifiedOn">The modified on.</param>
        /// <param name="deadline">The deadline.</param>
        public Assignment(
            int id,
            int ownerId,
            List<string> tags,
            string name,
            string description,
            List<Attachment> attachments,
            DateTime createdOn,
            DateTime modifiedOn,
            DateTime deadline)
        {
            this.Id = id;
            this.OwnerId = ownerId;
            this.Tags = tags;
            this.Name = name;
            this.Description = description;
            this.Attachments = attachments;
            this.CreatedOn = createdOn;
            this.ModifiedOn = modifiedOn;
            this.Deadline = deadline;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        public int OwnerId { get; private set; }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public List<Attachment> Attachments { get; private set; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime ModifiedOn { get; private set; }

        /// <summary>
        /// Gets the deadline.
        /// </summary>
        /// <value>
        /// The deadline.
        /// </value>
        public DateTime Deadline { get; private set; }
    }
}
