namespace AssignmentManager.DB.DBContext
{
    using AssignmentManager.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    /// <inheritdoc/>
    internal class DataContext : DbContext, IDataContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        public DataContext()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        public DbSet<Assignment> Assignments { get; set; }

        /// <inheritdoc/>
        public DbSet<Attachment> Attachments { get; set; }

        /// <inheritdoc/>
        public DbSet<Role> Roles { get; set; }

        /// <inheritdoc/>
        public DbSet<User> Users { get; set; }

        /// <inheritdoc/>
        public DbSet<Service> Services { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();

            // Adding postgres DB from connection string in appsettings file.
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
