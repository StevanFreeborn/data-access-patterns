namespace MyShop.Domain.Models;

public class Product
{
  public Guid ProductId { get; private set; }

  public string Name { get; set; } = string.Empty;

  public decimal Price { get; set; }

  public Product()
  {
    ProductId = Guid.NewGuid();
  }
}