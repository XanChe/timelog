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
        protected Func<T, bool>? predicate;
        protected IQueryable<T> Items 
        {
            get
            {
                if (predicate == null)
                {
                    return items;
                }
                else
                {                   
                    return items.Where(predicate).AsQueryable();
                }
            }
        }
        public Guid UserGuid { get { return userGuid; } }

        public DbRepositoryGeneric(DbContext context)
        {
            _context = context;
            this.items = context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return items.Where(item => item.UserUniqId == userGuid);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await items.Where(item => item.UserUniqId == userGuid).ToListAsync();
        }      
        public void Create(T item)
        {
            item.UserUniqId = userGuid;
            items.Add(item);
        }
        public async Task CreateAsync(T item)
        {
            item.UserUniqId = userGuid;
            await items.AddAsync(item);
        }
        public void Delete(Guid id)
        {
            
            var item = Read(id);

            if (item != null)
            {
                items.Remove(item);
            }            
        }

        public T? Read(Guid id)
        {
            return items
                .Where(item => item.UserUniqId == userGuid)
                .Where(item => item.Id == id)
                .FirstOrDefault();
        }

        public async Task<T?> ReadAsync(Guid id)
        {
            return await items
                .Where(item => item.UserUniqId == userGuid)
                .Where(item => item.Id == id)
                .FirstOrDefaultAsync();
        }

        public void Update(T item)
        {
            item.UserUniqId = userGuid;
            items.Update(item);
        }
        public Task UpdateAsync(T item)
        {
            item.UserUniqId = userGuid;
            items.Update(item);
            return Task.CompletedTask;
        }

        public async Task<long> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void UseFilter(Func<T, bool> filter)
        {
            predicate = filter;
        }

        public void SetUser(Guid userIdentityGuid)
        {
            userGuid = userIdentityGuid;
            UseFilter(item => item.UserUniqId == userIdentityGuid);
        }
    }
}
