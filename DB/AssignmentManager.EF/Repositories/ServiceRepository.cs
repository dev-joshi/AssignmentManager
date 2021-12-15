namespace AssignmentManager.DB.EF.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AssignmentManager.DB.DBContext;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities;
    using Microsoft.EntityFrameworkCore;

    internal class ServiceRepository : IServiceRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly IDataContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ServiceRepository(IDataContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await this.context.Services.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Service> GetServiceAsync(int id)
        {
            return await this.context.Services.FindAsync(id);
        }
    }
}
