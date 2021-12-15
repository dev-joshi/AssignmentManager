namespace AssignmentManager.Entities
{
    /// <summary>
    /// Attachment.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Gets or Sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or Sets the size of the file.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        public int FileSize { get; set; }
    }
}
