namespace AssignmentManager.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or Sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or Sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or Sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or Sets the notification methods.
        /// </summary>
        /// <value>
        /// The notification methods.
        /// </value>
        public List<NotificationMethod> NotificationMethods { get; set; }

        /// <summary>
        /// Gets or Sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets the contact number.
        /// </summary>
        /// <value>
        /// The contact number.
        /// </value>
        public string ContactNumber { get; set; }
    }
}
