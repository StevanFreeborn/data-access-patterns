using MyShop.Domain.Models;
using MyShop.Infrastructure.Services;

namespace MyShop.Infrastructure.Lazy;

public class CustomerProxy : Customer
{
  public override byte[] ProfilePicture
  {
    get
    {
      return base.ProfilePicture ??= ProfilePictureService.GetProfilePicture(Name);
    }
  }

  internal static CustomerProxy FromCustomer(Customer customer)
  {
    return new CustomerProxy
    {
      CustomerId = customer.CustomerId,
      Name = customer.Name,
      City = customer.City,
      PostalCode = customer.PostalCode,
      ShippingAddress = customer.ShippingAddress,
      Country = customer.Country
    };
  }
}