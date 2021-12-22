namespace AssignmentManager.DB.EF.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    internal class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private IDataContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RoleRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await this.dataContext.Roles.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Role> GetRoleAsync(int id)
        {
            return await this.dataContext.Roles.FindAsync(id);
        }
    }
}
