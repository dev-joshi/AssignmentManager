namespace AssignmentManager.Entities.QueueMessage
{
    public class AssignmentCreatedMessage : BaseMessage
    {
        /// <summary>
        /// Gets or sets the assignment identifier.
        /// </summary>
        /// <value>
        /// The assignment identifier.
        /// </value>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the assignment name.
        /// </summary>
        /// <value>
        /// The assignment name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentCreatedMessage"/> class.
        /// </summary>
        /// <param name="id">The assignment identifier.</param>
        /// <param name="name">The assignment name.</param>
        public AssignmentCreatedMessage(int id, string name) : base()
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
