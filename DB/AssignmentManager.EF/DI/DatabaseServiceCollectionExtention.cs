namespace AssignmentManager.DB.EF.DI
{
    using AssignmentManager.DB.DBContext;
    using AssignmentManager.DB.EF.Repositories;
    using AssignmentManager.DB.Storage;
    using AssignmentManager.DB.Storage.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// DI extentions for adding Database.
    /// </summary>
    public static class DatabaseServiceCollectionExtention
    {
        /// <summary>
        /// Adds the database related services.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>service collection.</returns>
        public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DataContext>();
            serviceCollection.TryAddSingleton<IDataContext>(serviceProvider => serviceProvider.GetService<DataContext>());
            serviceCollection.TryAddTransient<IDatabaseSetup, DatabaseSetup>();

            serviceCollection.AddRepositories();

            return serviceCollection;
        }

        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>service collection.</returns>
        private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<IAssignmentRepository, AssignmentRepository>();
            serviceCollection.TryAddTransient<IServiceRepository, ServiceRepository>();
            serviceCollection.TryAddTransient<IUserRepository, UserRepository>();
            serviceCollection.TryAddTransient<IRoleRepository, RoleRepository>();
            serviceCollection.TryAddTransient<IKeyRepository, KeyRepository>();

            return serviceCollection;
        }
    }
}
