using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace MyShop.Infrastructure.Repositories;

public abstract class GenericRepository<T>(ShoppingContext context) : IRepository<T> where T : class
{
  private protected readonly ShoppingContext _context = context;

  public virtual async Task<T> AddAsync(T entity)
  {
    var result = await _context.AddAsync(entity);
    return result.Entity;
  }

  public virtual async Task<IEnumerable<T>> All()
  {
    return await _context.Set<T>().ToListAsync();
  }

  public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
  {
    return await _context.Set<T>().Where(predicate).ToListAsync();
  }

  public virtual async Task<T?> Get(Guid id)
  {
    return await _context.Set<T>().FindAsync(id);
  }

  public virtual async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }

  public virtual Task<T> UpdateAsync(T entity)
  {
    var result = _context.Update(entity);
    return Task.FromResult(result.Entity);
  }
}