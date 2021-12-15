namespace AssignmentManager.DB.Storage.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AssignmentManager.Entities;

    /// <summary>
    /// Role Repository.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The role.</returns>
        Task<Role> GetRoleAsync(int id);

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>The roles.</returns>
        Task<IEnumerable<Role>> GetAllRolesAsync();
    }
}
