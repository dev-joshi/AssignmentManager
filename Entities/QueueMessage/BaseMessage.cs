namespace AssignmentManager.Entities.QueueMessage
{
    using System;

    /// <summary>
    /// Base Message for queue.
    /// </summary>
    public class BaseMessage
    {
        /// <summary>
        /// Gets or sets the published datetime.
        /// </summary>
        /// <value>
        /// The published datetime.
        /// </value>
        public DateTime Published { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMessage"/> class.
        /// </summary>
        public BaseMessage()
        {
            Published = DateTime.UtcNow;
        }
    }
}
