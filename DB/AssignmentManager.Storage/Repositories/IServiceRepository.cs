namespace AssignmentManager.DB.Storage.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AssignmentManager.Entities;

    /// <summary>
    /// Service Repository.
    /// </summary>
    public interface IServiceRepository
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The service.</returns>
        Task<Service> GetServiceAsync(int id);

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The service.</returns>
        Task<Service> GetServiceAsync(string name);

        /// <summary>
        /// Gets all services.
        /// </summary>
        /// <returns>The services.</returns>
        Task<IEnumerable<Service>> GetAllServicesAsync();
    }
}
