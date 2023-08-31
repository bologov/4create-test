using Domain.Entities;
using Domain.Specification;

namespace Domain.Repository
{
    public interface IRepository<TEntity, T> where TEntity : Entity<T>
    {
        Task<TEntity> FindByIdAsync(T id);
        Task<TEntity> FindOneAsync(Specification<TEntity> spec);
        Task<IEnumerable<TEntity>> FindAsync(Specification<TEntity> spec);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}

