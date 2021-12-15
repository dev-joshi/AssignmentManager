namespace AssignmentManager.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Result.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or Sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the assignment identifier.
        /// </summary>
        /// <value>
        /// The assignment identifier.
        /// </value>
        public int AssignmentId { get; set; }

        /// <summary>
        /// Gets or Sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

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
        /// Gets or Sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
