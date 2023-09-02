using System.Linq.Expressions;

namespace Domain.Specification
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> Expression { get; }

        public virtual IQueryable<T> ExtendQuery(IQueryable<T> query)
        {
            return query;
        }

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = Expression.Compile();
            return predicate(entity);
        }
    }
}

