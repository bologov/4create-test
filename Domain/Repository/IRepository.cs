using Domain.Entities;
using Domain.Specification;

namespace Domain.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        Task<TEntity> FindAsync(TKey id);
        Task<TEntity?> FindOrDefaultAsync(Specification<TEntity> spec);
        Task<IEnumerable<TEntity>> FindManyAsync(Specification<TEntity> spec, string include = null);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}

