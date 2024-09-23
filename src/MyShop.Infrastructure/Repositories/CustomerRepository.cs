using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy;

namespace MyShop.Infrastructure.Repositories;

public class CustomerRepository(ShoppingContext context) : GenericRepository<Customer>(context)
{
  public override async Task<IEnumerable<Customer>> All()
  {
    var customers = await base.All();
    return customers.Select(c => new CustomerProxy(c));
  }

  public override Task<Customer> UpdateAsync(Customer entity)
  {
    var customer = _context.Customers.Single(c => c.CustomerId == entity.CustomerId);

    customer.Name = entity.Name;
    customer.City = entity.City;
    customer.PostalCode = entity.PostalCode;
    customer.ShippingAddress = entity.ShippingAddress;
    customer.Country = entity.Country;

    return base.UpdateAsync(customer);
  }
}