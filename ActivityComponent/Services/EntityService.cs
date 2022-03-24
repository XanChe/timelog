using System;
using System.Collections.Generic;
using Timelog.Entities;
using Timelog.Interfaces;

namespace Timelog.Services
{
    public class EntityService<T> where T : Entity 
    {
        private IRepositoryGeneric<T> _repository;

        public EntityService(IRepositoryGeneric<T> repository)
        {
            _repository = repository;
        }
        public IEnumerable<T> GetItems()
        {
            return _repository.GetAll();
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("added item is empty");
            }
            _repository.Create(item);   
            _repository.SaveChanges();
        }

        public void Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("updated item is empty");
            }
            _repository.Update(item);
            _repository.SaveChanges();
        }
    }
}
