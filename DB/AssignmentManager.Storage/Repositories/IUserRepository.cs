namespace AssignmentManager.DB.Storage.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AssignmentManager.Entities;

    /// <summary>
    /// User Repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The user.</returns>
        Task<User> GetUserAsync(int id);

        /// <summary>
        /// Gets the user by name.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user.</returns>
        Task<User> GetUserAsync(string username);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>The users.</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Add or update the given user.
        /// </summary>
        /// <param name="assignment">The user.</param>
        /// <returns>Task representing async operation.</returns>
        Task AddOrUpdateAsync(User user);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task representing async operation.</returns>
        Task DeleteAsync(int id);
    }
}
