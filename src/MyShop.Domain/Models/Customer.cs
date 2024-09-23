using MyShop.Domain.Lazy;

namespace MyShop.Domain.Models;

public class Customer
{
  public Guid CustomerId { get; set; }

  public string Name { get; set; } = string.Empty;
  public string ShippingAddress { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string PostalCode { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;

  // NOTE: This is not thread-safe. Should instead use built-in Lazy<T> class.
  public IValueHolder<byte[]> ProfilePictureValueHolder { get; set; } = null!;

  public virtual byte[] ProfilePicture
  {
    get
    {
      return ProfilePictureValueHolder.GetValue(Name);
    }

    set { }
  }

  public Customer()
  {
    CustomerId = Guid.NewGuid();
  }
}