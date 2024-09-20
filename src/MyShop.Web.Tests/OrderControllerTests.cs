using Moq;

using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Controllers;
using MyShop.Web.Models;

namespace MyShop.Web.Tests;

public class UnitTest1
{
  [Fact]
  public async Task CanCreateOrderWithCorrectModel()
  {
    var mockOrderRepository = new Mock<IRepository<Order>>();
    var mockCustomerRepository = new Mock<IRepository<Customer>>();
    var mockProductRepository = new Mock<IRepository<Product>>();
    var unitOfWork = new Mock<IUnitOfWork>();

    unitOfWork
      .Setup(x => x.OrderRepository)
      .Returns(mockOrderRepository.Object);

    unitOfWork
      .Setup(x => x.CustomerRepository)
      .Returns(mockCustomerRepository.Object);

    unitOfWork
      .Setup(x => x.ProductRepository)
      .Returns(mockProductRepository.Object);

    var controller = new OrderController(unitOfWork.Object);

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

    unitOfWork.Verify(x => x.OrderRepository.AddAsync(It.IsAny<Order>()), Times.Once);
  }
}