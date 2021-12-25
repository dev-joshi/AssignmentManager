namespace AssignmentManager.DB.EF.Repositories
{
    using AssignmentManager.DB.Storage.Repositories;
    using AssignmentManager.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <inheritdoc />
    internal class KeyRepository : IKeyRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly IDataContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public KeyRepository(
            IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <inheritdoc />
        public async Task AddOrUpdateKeyAsync(Key key)
        {
            var existing = await this.dataContext.Keys.FindAsync(key.Id);

            if (existing is not null)
            {
                key.CreatedOn = DateTime.UtcNow;
                this.dataContext.Keys.Update(key);
            }
            else
            {
                key.CreatedOn = DateTime.UtcNow;
                await this.dataContext.Keys.AddAsync(key);
            }

            await this.dataContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteKeyAsync(int id)
        {
            var existing = await this.dataContext.Keys.FindAsync(id);

            if (existing is null)
            {
                throw new NullReferenceException($"No Key exists with given id {id}");
            }

            this.dataContext.Keys.Remove(existing);
            await this.dataContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteKeyAsync(string name)
        {
            var existing = this.dataContext.Keys.FirstOrDefault(x => x.Name == name);

            if (existing is null)
            {
                throw new NullReferenceException($"No Key exists with given name {name}");
            }

            this.dataContext.Keys.Remove(existing);
            await this.dataContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Key>> GetAllKeys()
        {
            return await this.dataContext.Keys.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Key> GetKeyAsync(int id)
        {
            return await this.dataContext.Keys.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task<Key> GetKeyAsync(string name)
        {
            return await this.dataContext.Keys.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
