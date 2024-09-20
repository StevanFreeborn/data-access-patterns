using System.Diagnostics;
using System.Linq.Expressions;

using Microsoft.AspNetCore.Mvc;

using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Web.Models;

namespace MyShop.Web.Controllers;

public class OrderController(IUnitOfWork unitOfWork) : Controller
{
  private readonly IUnitOfWork _unitOfWork = unitOfWork;

  public async Task<IActionResult> Index()
  {
    Expression<Func<Order, bool>> criteria = order => order.OrderDate > DateTime.UtcNow.AddDays(-1);
    var orders = await _unitOfWork.OrderRepository.Find(criteria);
    return View(orders);
  }

  public async Task<IActionResult> Create()
  {
    var products = await _unitOfWork.ProductRepository.All();
    return View(products);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateOrderModel model)
  {
    if (model.LineItems.Count is 0) return BadRequest("Please submit line items");

    if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");

    var customers = await _unitOfWork.CustomerRepository.Find(c => c.Name == model.Customer.Name);
    var customer = customers.FirstOrDefault();

    if (customer is not null)
    {
      customer.ShippingAddress = model.Customer.ShippingAddress;
      customer.City = model.Customer.City;
      customer.PostalCode = model.Customer.PostalCode;
      customer.Country = model.Customer.Country;

      await _unitOfWork.CustomerRepository.UpdateAsync(customer);
    }
    else
    {
      customer = new Customer
      {
        Name = model.Customer.Name,
        ShippingAddress = model.Customer.ShippingAddress,
        City = model.Customer.City,
        PostalCode = model.Customer.PostalCode,
        Country = model.Customer.Country
      };

      await _unitOfWork.CustomerRepository.AddAsync(customer);
    }

    var order = new Order
    {
      LineItems = model.LineItems
        .Select(line => new LineItem { ProductId = line.ProductId, Quantity = line.Quantity })
        .ToList(),

      Customer = customer
    };

    await _unitOfWork.OrderRepository.AddAsync(order);
    await _unitOfWork.SaveChangesAsync();
    return Ok("Order Created");
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}