using Microsoft.EntityFrameworkCore;

using MyShop.Domain.Lazy;
using MyShop.Domain.Models;
using MyShop.Infrastructure.Services;

namespace MyShop.Infrastructure.Repositories;

public class CustomerRepository(ShoppingContext context) : GenericRepository<Customer>(context)
{
  public override async Task<IEnumerable<Customer>> All()
  {
    var customers = await base.All();

    return customers.Select(c =>
    {
      c.ProfilePictureValueHolder = new ValueHolder<byte[]>(parameter =>
      {
        return ProfilePictureService.GetProfilePicture(parameter.ToString() ?? string.Empty);
      });

      return c;
    });
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