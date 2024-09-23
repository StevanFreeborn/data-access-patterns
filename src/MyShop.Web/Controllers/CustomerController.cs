using Microsoft.AspNetCore.Mvc;

using MyShop.Infrastructure;

namespace MyShop.Web.Controllers;

public class CustomerController(IUnitOfWork uow) : Controller
{
  private readonly IUnitOfWork _uow = uow;

  public async Task<IActionResult> Index(Guid? id)
  {
    if (id == null)
    {
      var customers = await _uow.CustomerRepository.All();

      return View(customers.ToList());
    }
    else
    {
      var customer = await _uow.CustomerRepository.Find(c => c.CustomerId == id);

      return View(new[] { customer });
    }
  }
}