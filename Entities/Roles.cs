namespace AssignmentManager.Entities
{
    using System.ComponentModel;

    public enum Roles
    {
        /// <summary>
        /// No Role Selected.
        /// </summary>
        None = 0,

        /// <summary>
        /// Get Any User's Details.
        /// </summary>
        [Description("View Any User's Details")]
        ViewUserData = 1,

        /// <summary>
        /// Edit Any User's Details.
        /// </summary>
        [Description("Edit Any User's Details")]
        EditUserData = 2,

        /// <summary>
        /// Create Assignment.
        /// </summary>
        [Description("Create Assignment")]
        CreateAssignment = 3,

        /// <summary>
        /// View Assignment.
        /// </summary>
        [Description("View Assignment")]
        ViewAssignment = 4,

        /// <summary>
        /// Submit Assignment.
        /// </summary>
        [Description("Submit Assignment")]
        SubmitAssignment = 5,

        /// <summary>
        /// View Assignments created by other users.
        /// </summary>
        [Description("View Assignments created by other users")]
        ViewAllAssignment = 6,

        /// <summary>
        /// Edit Assignments created by other users.
        /// </summary>
        [Description("Edit Assignments created by other users")]
        EditAllAssignment = 7,

        /// <summary>
        /// Connect to Queue to Publish and subscribe Messages.
        /// </summary>
        [Description("Publish and subscribe for Messages to Queue")]
        ConnectToMessageQueue = 8,
    }
}
