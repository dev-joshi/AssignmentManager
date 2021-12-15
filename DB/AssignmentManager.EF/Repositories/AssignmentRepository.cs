namespace AssignmentManager.DB.EF.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AssignmentManager.DB.DBContext;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc/>
    internal class AssignmentRepository : IAssignmentRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly IDataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AssignmentRepository(IDataContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task AddOrUpdateAsync(Assignment assignment)
        {
            var existing = await this.context.Assignments.FindAsync(assignment.Id);

            if (existing is not null)
            {
                assignment.ModifiedOn = DateTime.UtcNow;
                this.context.Assignments.Update(assignment);
            }
            else
            {
                assignment.CreatedOn = DateTime.UtcNow;
                await this.context.Assignments.AddAsync(assignment);
            }

            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var existing = await this.context.Assignments.FindAsync(id);

            if (existing is null)
            {
                throw new NullReferenceException($"No Assignment exists with given id {id}");
            }

            this.context.Assignments.Remove(existing);
            await this.context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Assignment>> GetAllAssignmentsAsync()
        {
            return await this.context.Assignments.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Assignment> GetAssignmentAsync(int id)
        {
            return await this.context.Assignments.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Assignment>> GetAssignmentByOwnerAsync(int id)
        {
            return await this.context.Assignments.Where(x => x.OwnerId == id).ToListAsync();
        }
    }
}
