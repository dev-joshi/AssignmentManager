namespace AssignmentManager.DB.DI
{
    using System;
    using System.Threading.Tasks;
    using AssignmentManager.DB.DBContext;
    using AssignmentManager.DB.EF.Repositories;
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities;
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
                    context.Database.EnsureCreated();
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

            // Seed some test data.
            serviceProvider.GetService<DataContext>().SeedData();
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

            return serviceCollection;
        }

        /// <summary>
        /// Seeds test data.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        private static async void SeedData(this DataContext dataContext)
        {
            if ((await dataContext.Roles.FindAsync(1)) == null)
            {
                dataContext.Roles.Add(new Role { Id = 1, Name = "Role1" });
            }

            if ((await dataContext.Roles.FindAsync(2)) == null)
            {
                dataContext.Roles.Add(new Role { Id = 2, Name = "Role2" });
            }

            if ((await dataContext.Roles.FindAsync(3)) == null)
            {
                dataContext.Roles.Add(new Role { Id = 3, Name = "Role3" });
            }

            if ((await dataContext.Users.FindAsync(1)) == null)
            {
                dataContext.Users.Add(new User { Id = 1, Name = "Test User 1", UserName = "TestUser1", PasswordHash = "abc".Hash() });
            }

            if ((await dataContext.Services.FindAsync(1)) == null)
            {
                dataContext.Services.Add(new Service { Id = 1, ServiceName = "Test Service 1" });
            }
        }
    }
}
