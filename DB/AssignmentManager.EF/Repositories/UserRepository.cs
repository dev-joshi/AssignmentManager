namespace AssignmentManager.DB.EF.Repositories
{
    using System;
    using AssignmentManager.Entities;
    using AssignmentManager.DB.Storage.Repositories;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    internal class UserRepository : IUserRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private IDataContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UserRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <inheritdoc />
        public async Task AddOrUpdateAsync(User user)
        {
            var existing = await this.dataContext.Users.FindAsync(user.Id);

            if (existing is not null)
            {
                user.ModifiedOn = DateTime.UtcNow;
                this.dataContext.Users.Update(user);
            }
            else
            {
                user.CreatedOn = DateTime.UtcNow;
                await this.dataContext.Users.AddAsync(user);
            }

            await this.dataContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var existing = await this.dataContext.Users.FindAsync(id);

            if (existing is null)
            {
                throw new NullReferenceException($"No User exists with given id {id}");
            }

            this.dataContext.Users.Remove(existing);
            await this.dataContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await this.dataContext.Users.Include(u => u.Roles).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<User> GetUserAsync(int id)
        {
            return await this.dataContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<User> GetUserAsync(string username)
        {
            return await this.dataContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
