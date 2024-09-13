using System.Diagnostics;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Mvc;

using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Models;

namespace MyShop.Web.Controllers;

public class OrderController(IRepository<Order> orderRepository, IRepository<Product> productRepository) : Controller
{
  private readonly IRepository<Order> _orderRepository = orderRepository;
  private readonly IRepository<Product> _productRepository = productRepository;

  public async Task<IActionResult> Index()
  {
    Expression<Func<Order, bool>> criteria = order => order.OrderDate > DateTime.UtcNow.AddDays(-1);
    var orders = await _orderRepository.Find(criteria);
    return View(orders);
  }

  public async Task<IActionResult> Create()
  {
    var products = await _productRepository.All();
    return View(products);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateOrderModel model)
  {
    if (!model.LineItems.Any()) return BadRequest("Please submit line items");

    if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");

    var customer = new Customer
    {
      Name = model.Customer.Name,
      ShippingAddress = model.Customer.ShippingAddress,
      City = model.Customer.City,
      PostalCode = model.Customer.PostalCode,
      Country = model.Customer.Country
    };

    var order = new Order
    {
      LineItems = model.LineItems
        .Select(line => new LineItem { ProductId = line.ProductId, Quantity = line.Quantity })
        .ToList(),

      Customer = customer
    };

    await _orderRepository.AddAsync(order);
    await _orderRepository.SaveChangesAsync();
    return Ok("Order Created");
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}