using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using MyShop.Domain.Models;

namespace MyShop.Infrastructure.Repositories;

public class OrderRepository(ShoppingContext context) : GenericRepository<Order>(context)
{
  public override async Task<IEnumerable<Order>> Find(Expression<Func<Order, bool>> predicate)
  {
    return await _context.Orders
      .Include(o => o.LineItems)
      .ThenInclude(l => l.Product)
      .Where(predicate)
      .ToListAsync();
  }

  public override Task<Order> UpdateAsync(Order entity)
  {
    var order = _context.Orders
      .Include(o => o.LineItems)
      .ThenInclude(l => l.Product)
      .Single(o => o.OrderId == entity.OrderId);

    order.OrderDate = entity.OrderDate;
    order.LineItems = entity.LineItems;

    return base.UpdateAsync(entity);
  }
}