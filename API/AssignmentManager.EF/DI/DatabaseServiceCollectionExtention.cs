namespace AssignmentManager.DB.DI
{
    using System;
    using AssignmentManager.DB.DBContext;
    using AssignmentManager.DB.Repositories;
    using Microsoft.EntityFrameworkCore;
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

            serviceCollection.AddRepositories();

            return serviceCollection;
        }

        /// <summary>
        /// Migrates the database.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetService<DataContext>())
            {
                context.Database.Migrate();
            }
        }

        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>service collection.</returns>
        private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAssignmentRepository, AssignmentRepository>();

            return serviceCollection;
        }
    }
}
