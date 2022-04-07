using System;
using System.Collections.Generic;
using System.Text;
using Timelog.Core.Entities;

namespace Timelog.Core.Repositories
{
    public interface IRepositoryGeneric<T> where T : Entity
    {
        public void UseFilter(Func<T, bool> filter);

        public void SetUser(Guid userIdentityGuid);
        public Guid UserGuid { get; }
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T? Read(long id);
        Task<T?> ReadAsync(long id);
        void Create(T item);
        Task CreateAsync(T item);
        void Update(T item);
        Task UpdateAsync(T item);
        void Delete(long id);
        Task<long> SaveChangesAsync();

    }

}
