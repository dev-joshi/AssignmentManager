namespace AssignmentManager.DB.DI
{
    using System;
    using System.Threading.Tasks;
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
        public static void SetupDB(this IServiceProvider serviceProvider)
        {
            // Setup DB with 3 attempts 10 seconds apart.
            var attemptsLeft = 3;

            Console.WriteLine($"Setting up DB with {attemptsLeft} attemps");

            while (attemptsLeft > 0)
            {
                try
                {
                    var context = serviceProvider.GetService<DataContext>();
                    context.Database.Migrate();
                    Console.WriteLine("DB Setup Completed");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to Setup DB : {ex}, Attempts Left : {--attemptsLeft}");

                    if (attemptsLeft <= 0)
                    {
                        throw;
                    }
                    else
                    {
                        Task.Delay(TimeSpan.FromSeconds(10)).ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                }
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
