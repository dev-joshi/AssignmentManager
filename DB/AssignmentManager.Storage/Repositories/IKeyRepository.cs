namespace AssignmentManager.DB.Storage.Repositories
{
    using AssignmentManager.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Key Repository.
    /// </summary>
    public interface IKeyRepository
    {
        /// <summary>
        /// Add or update the given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Task representing asyncronous operation.</returns>
        Task AddOrUpdateKeyAsync(Key key);

        /// <summary>
        /// Deletes the key.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Task representing asyncronous operation.</returns>
        Task DeleteKeyAsync(int id);

        /// <summary>
        /// Deletes the key.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Task representing asyncronous operation.</returns>
        Task DeleteKeyAsync(string name);

        /// <summary>
        /// Gets the key for specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The Key.</returns>
        Task<Key> GetKeyAsync(int id);

        /// <summary>
        /// Gets the key for specifed name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The Key.</returns>
        Task<Key> GetKeyAsync(string name);

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <returns>all Keys.</returns>
        Task<IEnumerable<Key>> GetAllKeys();
    }
}
