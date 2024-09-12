namespace MyShop.Domain.Models;

public class Order
{
  public Guid OrderId { get; private set; }

  public virtual ICollection<LineItem> LineItems { get; set; } = null!;

  public virtual Customer Customer { get; set; } = null!;
  public Guid CustomerId { get; set; }

  // SQLite doesn't support DateTimeOffset :(
  public DateTime OrderDate { get; set; }

  public decimal OrderTotal => LineItems.Sum(item => item.Product.Price * item.Quantity);

  public Order()
  {
    OrderId = Guid.NewGuid();
    OrderDate = DateTime.UtcNow;
  }
}