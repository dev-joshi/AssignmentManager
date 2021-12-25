namespace AssignmentManager.DB.EF.DI
{
    using AssignmentManager.DB.DBContext;
    using AssignmentManager.DB.EF.Repositories;
    using AssignmentManager.DB.Storage;
    using AssignmentManager.DB.Storage.Repositories;
    using Microsoft.Extensions.DependencyInjection;

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
            serviceCollection.AddSingleton<IDataContext>(serviceProvider => serviceProvider.GetService<DataContext>());
            serviceCollection.AddTransient<IDatabaseSetup, DatabaseSetup>();

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
            serviceCollection.AddTransient<IAssignmentRepository, AssignmentRepository>();
            serviceCollection.AddTransient<IServiceRepository, ServiceRepository>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<IRoleRepository, RoleRepository>();
            serviceCollection.AddTransient<IKeyRepository, KeyRepository>();

            return serviceCollection;
        }
    }
}
