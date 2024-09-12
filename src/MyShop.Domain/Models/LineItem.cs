namespace MyShop.Domain.Models;

public class LineItem
{
  public Guid LineItemId { get; set; }

  public int Quantity { get; set; }

  public virtual Product Product { get; set; } = null!;
  public Guid ProductId { get; set; }

  public virtual Order Order { get; set; } = null!;
  public Guid OrderId { get; set; }

  public LineItem()
  {
    LineItemId = Guid.NewGuid();
  }
}