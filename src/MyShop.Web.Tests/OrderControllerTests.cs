using Moq;

using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Controllers;
using MyShop.Web.Models;

namespace MyShop.Web.Tests;

public class UnitTest1
{
  [Fact]
  public async Task CanCreateOrderWithCorrectModel()
  {
    var orderRepo = new Mock<IRepository<Order>>();
    var productRepo = new Mock<IRepository<Product>>();

    var controller = new OrderController(orderRepo.Object, productRepo.Object);

    var createOrderModel = new CreateOrderModel()
    {
      Customer = new CustomerModel()
      {
        Name = "John Doe",
        ShippingAddress = "123 Elm St",
        City = "Springfield",
        PostalCode = "12345"
      },
      LineItems = [
        new LineItemModel() { ProductId = Guid.NewGuid(), Quantity = 2 },
        new LineItemModel() { ProductId = Guid.NewGuid(), Quantity = 1 }
      ],
    };

    await controller.Create(createOrderModel);

    orderRepo.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
  }
}