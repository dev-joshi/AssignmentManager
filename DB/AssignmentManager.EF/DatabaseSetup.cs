namespace AssignmentManager.DB.EF
{
    using AssignmentManager.Common;
    using AssignmentManager.DB.Storage;
    using AssignmentManager.Entities;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <inheritdoc />
    public class DatabaseSetup : IDatabaseSetup
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly IDataContext dataContext;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<DatabaseSetup> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSetup"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="logger">The logger.</param>
        public DatabaseSetup(
            IDataContext dataContext,
            ILogger<DatabaseSetup> logger)
        {
            this.dataContext = dataContext;
            this.logger = logger;
        }

        /// <inheritdoc />
        public void SetupDatabase()
        {
            // Setup DB with 5 attempts 10 seconds apart.
            var attemptsLeft = 5;

            this.logger.LogInformation("Setting up DB with {attemptsLeft} attemps", attemptsLeft);

            while (attemptsLeft > 0)
            {
                try
                {
                    dataContext.Migrate();
                    this.logger.LogInformation("DB Setup Completed");
                    break;
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "Failed to Setup DB, Attempts Left : {attemptsLeft}", --attemptsLeft);

                    if (attemptsLeft <= 0)
                    {
                        throw;
                    }
                    else
                    {
                        Task.Delay(TimeSpan.FromSeconds(15)).ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                }
            }
        }

        /// <inheritdoc />
        public void SeedData()
        {
            try
            {
                if (this.dataContext.Users == null
                    || this.dataContext.Users.Count() == 0)
                {
                    this.logger.LogInformation("Seeding data");

                    this.dataContext.Roles.Add(new Role { Id = 1, Name = "Role 1", Services = new List<Service>(), Users = new List<User>() });
                    this.dataContext.Roles.Add(new Role { Id = 2, Name = "Role 2", Services = new List<Service>(), Users = new List<User>() });
                    this.dataContext.Roles.Add(new Role { Id = 3, Name = "Role 3", Services = new List<Service>(), Users = new List<User>() });
                    this.dataContext.SaveChanges();
                    
                    this.dataContext.Services.Add(new Service { Id = 1, ServiceName = "Test Service 1", Roles = new List<Role>() });
                    this.dataContext.Users.Add(new User { Id = 1, Name = "Test User 1", UserName = "TestUser", PasswordHash = "abc".Hash(), Roles = new List<Role>() });
                    this.dataContext.SaveChanges();
                    
                    this.dataContext.Services.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == 1));
                    this.dataContext.Services.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == 2));
                    this.dataContext.SaveChanges();

                    this.dataContext.Roles.First(x => x.Id == 1).Services.Add(this.dataContext.Services.First(x => x.Id == 1));
                    this.dataContext.Roles.First(x => x.Id == 2).Services.Add(this.dataContext.Services.First(x => x.Id == 1));
                    this.dataContext.SaveChanges();

                    this.dataContext.Users.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == 2));
                    this.dataContext.Users.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == 3));
                    this.dataContext.SaveChanges();

                    this.dataContext.Roles.First(x => x.Id == 2).Users.Add(this.dataContext.Users.First(x => x.Id == 1));
                    this.dataContext.Roles.First(x => x.Id == 3).Users.Add(this.dataContext.Users.First(x => x.Id == 1));
                    this.dataContext.SaveChanges();

                    this.dataContext.Keys.Add(new Key { Id = 1, CreatedOn = DateTime.UtcNow, Name = "JwTSecretKey", Value = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==" });
                    this.dataContext.SaveChanges();

                    this.logger.LogInformation("Seeding complete");
                }
                else
                {
                    this.logger.LogInformation("User 1 already present, skipping seeding");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed while seeding data");
            }
        }
    }
}
