using Timelog.Core.Entities;

namespace Timelog.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(long Id);
        public Task Create(T item);
        public Task Update(T item);
        public Task Delete(long id);       
    }
}
