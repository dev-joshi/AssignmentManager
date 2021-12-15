namespace AssignmentManager.DB.DBContext
{
    using System.Threading;
    using System.Threading.Tasks;
    using AssignmentManager.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Data Context.
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Gets or sets the assignments.
        /// </summary>
        /// <value>
        /// The assignments.
        /// </value>
        DbSet<Assignment> Assignments { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        DbSet<Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        DbSet<User> Users { get; set; }

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task Representing an Async operation.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
