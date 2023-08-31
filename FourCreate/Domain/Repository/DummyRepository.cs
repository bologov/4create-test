using Domain.Entities;
using Domain.Specification;

namespace Domain.Repository
{
    public class DummyRepository<TEntity, T> : IRepository<TEntity, T> where TEntity : Entity<T>
    {
        public async Task<TEntity> FindByIdAsync(T id)
        {
            throw new NotImplementedException();
        }
        public async Task<TEntity> FindOneAsync(Specification<TEntity> spec)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Specification<TEntity> spec)
        {
            throw new NotImplementedException();
        }
        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

