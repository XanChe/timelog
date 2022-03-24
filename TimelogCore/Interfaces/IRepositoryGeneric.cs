using System;
using System.Collections.Generic;
using System.Text;
using Timelog.Entities;

namespace Timelog.Interfaces
{
    public interface IRepositoryGeneric<T> where T : Entity
    {
        public void UseFilter(Func<T, bool> filter);

        public void SetUser(Guid userIdentityGuid);
        public Guid UserGuid { get; }
        IEnumerable<T> GetAll();
        T Read(long id);
        void Create(T item);
        void Update(T item);
        void Delete(long id);
        long SaveChanges();

    }

}
