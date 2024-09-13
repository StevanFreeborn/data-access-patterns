using MyShop.Domain.Models;

namespace MyShop.Infrastructure.Repositories;

public class ProductRepository(ShoppingContext context) : GenericRepository<Product>(context)
{
  public override Task<Product> UpdateAsync(Product entity)
  {
    var product = _context.Products.Single(p => p.ProductId == entity.ProductId);

    product.Name = entity.Name;
    product.Price = entity.Price;

    return base.UpdateAsync(product);
  }
}