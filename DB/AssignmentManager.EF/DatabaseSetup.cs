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
            // Setup DB with 5 attempts 20 seconds apart.
            var attemptsLeft = 5;

            while (attemptsLeft > 0)
            {
                try
                {
                    this.logger.LogInformation("Setting up DB, attempt count : {attemptsLeft}", 6 - attemptsLeft);
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
                        Task.Delay(TimeSpan.FromSeconds(20)).ConfigureAwait(false).GetAwaiter().GetResult();
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

                    // Add all roles to database.
                    foreach (var e in Enum.GetValues<Roles>())
                    {
                        this.dataContext.Roles.Add(new Role { Id = e, Name = e.GetDescription(), Services = new List<Service>(), Users = new List<User>() });
                    }

                    this.dataContext.SaveChanges();
                    
                    this.dataContext.Services.Add(new Service { Id = 1, ServiceName = "Test Service 1", Roles = new List<Role>() });
                    this.dataContext.Users.Add(new User { Id = 1, Name = "Test User 1", UserName = "TestUser", PasswordHash = "abc".Hash(), Roles = new List<Role>() });
                    this.dataContext.SaveChanges();
                    
                    this.dataContext.Services.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == Roles.ViewUserData));
                    this.dataContext.Services.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == Roles.ViewAllAssignment));
                    this.dataContext.SaveChanges();

                    this.dataContext.Roles.First(x => x.Id == Roles.ViewUserData).Services.Add(this.dataContext.Services.First(x => x.Id == 1));
                    this.dataContext.Roles.First(x => x.Id == Roles.ViewAllAssignment).Services.Add(this.dataContext.Services.First(x => x.Id == 1));
                    this.dataContext.SaveChanges();

                    this.dataContext.Users.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == Roles.CreateAssignment));
                    this.dataContext.Users.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == Roles.ViewAssignment));
                    this.dataContext.Users.First(x => x.Id == 1).Roles.Add(this.dataContext.Roles.First(x => x.Id == Roles.SubmitAssignment));
                    this.dataContext.SaveChanges();

                    this.dataContext.Roles.First(x => x.Id == Roles.CreateAssignment).Users.Add(this.dataContext.Users.First(x => x.Id == 1));
                    this.dataContext.Roles.First(x => x.Id == Roles.ViewAssignment).Users.Add(this.dataContext.Users.First(x => x.Id == 1));
                    this.dataContext.Roles.First(x => x.Id == Roles.SubmitAssignment).Users.Add(this.dataContext.Users.First(x => x.Id == 1));
                    this.dataContext.SaveChanges();

                    this.dataContext.Keys.Add(new Key { Id = 1, CreatedOn = DateTime.UtcNow, Name = "JwTSecretKey", Value = ConfigurationConstants.JwTSecretKey });
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

        /// <inheritdoc />
        public bool CanConnect()
        {
            return this.dataContext.CanConnect();
        }
    }
}
