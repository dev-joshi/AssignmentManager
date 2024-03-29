﻿namespace AssignmentManager.DB.EF
{
    using AssignmentManager.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

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
        DbSet<Assignment> Assignments { get; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        DbSet<Attachment> Attachments { get; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        DbSet<Role> Roles { get; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        DbSet<User> Users { get; }

        /// <summary>
        /// Gets or sets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        DbSet<Service> Services { get; }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <value>
        /// The keys.
        /// </value>
        DbSet<Key> Keys { get; }

        /// <summary>
        /// Saves the changes async.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        /// <summary>
        /// Migrates the Database to current schema and creates if not created.
        /// </summary>
        void Migrate();

        /// <summary>
        /// Determines whether database can be connected.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can connect; otherwise, <c>false</c>.
        /// </returns>
        bool CanConnect();
    }
}
