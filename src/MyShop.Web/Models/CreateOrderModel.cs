namespace MyShop.Web.Models;

public class CreateOrderModel
{
  public List<LineItemModel> LineItems { get; set; } = [];

  public CustomerModel Customer { get; set; } = new CustomerModel();
}