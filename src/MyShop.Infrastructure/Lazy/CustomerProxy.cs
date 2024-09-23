using MyShop.Domain.Models;
using MyShop.Infrastructure.Services;

namespace MyShop.Infrastructure.Lazy;

public class CustomerProxy : Customer
{
  public CustomerProxy(Customer customer)
  {
    CustomerId = customer.CustomerId;
    Name = customer.Name;
    ShippingAddress = customer.ShippingAddress;
    City = customer.City;
    PostalCode = customer.PostalCode;
    Country = customer.Country;
  }

  public override byte[] ProfilePicture
  {
    get
    {
      return base.ProfilePicture ??= ProfilePictureService.GetProfilePicture(Name);
    }
  }
}