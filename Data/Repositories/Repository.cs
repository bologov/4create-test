using Domain.Entities;
using Domain.Repository;
using Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> FindAsync(TKey id)
        {
            return await _dbContext.FindAsync<TEntity>(id) ?? throw new ArgumentException($"Couldn't find {typeof(TEntity).Name} with id {id}");
        }

        public async Task<TEntity?> FindOrDefaultAsync(Specification<TEntity> spec)
        {
            return await _dbContext.Set<TEntity>().Where(spec.Expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> FindManyAsync(Specification<TEntity> spec, string include = null)
        {
            var query = _dbContext.Set<TEntity>().Where(spec.Expression);

            if (!string.IsNullOrEmpty(include))
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public void Add(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
    }
}

