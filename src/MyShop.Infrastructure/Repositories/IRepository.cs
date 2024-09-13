using System.Linq.Expressions;

namespace MyShop.Infrastructure.Repositories;

public interface IRepository<T>
{
  Task<T> AddAsync(T entity);
  Task<T> UpdateAsync(T entity);
  Task<T?> Get(Guid id);
  Task<IEnumerable<T>> All();
  Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
  Task SaveChangesAsync();
}