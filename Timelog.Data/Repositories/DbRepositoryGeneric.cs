using Microsoft.EntityFrameworkCore;
using Timelog.Core.Entities;
using Timelog.Core.Repositories;

namespace Timelog.Data.Repositories
{
    public class DbRepositoryGeneric<T> : IRepositoryGeneric<T> where T : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> items;
        protected Guid userGuid;
        protected bool isFilterNeed = false;
        
        public Guid UserGuid { get { return userGuid; } }

        public DbRepositoryGeneric(DbContext context)
        {
            _context = context;
            this.items = context.Set<T>();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            if (isFilterNeed)
            {
                return await items.Where(item => item.UserUniqId == userGuid).ToListAsync();
            }
            else
            {
                return await items.ToListAsync();
            }
        }
        public async Task CreateAsync(T item)
        {
            if (isFilterNeed)
            {
                item.UserUniqId = userGuid;
            }
            await items.AddAsync(item);
        }
        public async Task DeleteAsync(Guid id)
        {

            var item = await ReadAsync(id);

            if (item != null)
            {
                items.Remove(item);
            }
        }
        public async Task<T?> ReadAsync(Guid id)
        {            
            if (isFilterNeed)
            {
                return await items
                    .Where(item => item.UserUniqId == userGuid)
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return await items
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();
            }            
        }
        public Task UpdateAsync(T item)
        {
            if (isFilterNeed)
            {
                item.UserUniqId = userGuid;
            }
            items.Update(item);

            return Task.CompletedTask;
        }
        public async Task<long> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }       
        public void SetUser(Guid userIdentityGuid)
        {
            userGuid = userIdentityGuid;
            isFilterNeed = true;
            
        }
    }
}
