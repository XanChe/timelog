using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Timelog.Entities;
using Timelog.Interfaces;


namespace Timelog.Repositories
{
    public class DbRepositoryGeneric<T> : IRepositoryGeneric<T> where T : Entity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> items;
        protected Guid userGuid;
        protected Func<T, bool> predicate;
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
            return Items;
        }

        public void Create(T item)
        {
            item.UserUniqId = userGuid;
            items.Add(item);
        }

        public void Delete(long id)
        {
            
            var item = Read(id);

            if (item != null)
            {
                items.Remove(item);
            }            
        }
       
        public T Read(long id)
        {
           return Items.Where(item => item.Id == id).FirstOrDefault();
        }

        public void Update(T item)
        {
            item.UserUniqId = userGuid;
            items.Update(item);
        }

        public long SaveChanges()
        {
            return _context.SaveChanges();
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
