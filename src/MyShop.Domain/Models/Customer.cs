namespace MyShop.Domain.Models;

public class Customer
{
  public Guid CustomerId { get; set; }

  public string Name { get; set; } = string.Empty;
  public string ShippingAddress { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string PostalCode { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;

  private byte[]? _profilePicture;
  public byte[]? ProfilePicture
  {
    get
    {
      return _profilePicture ??= ProfilePictureService.GetProfilePicture(Name);
    }
    set
    {
      _profilePicture = value;
    }
  }

  public Customer()
  {
    CustomerId = Guid.NewGuid();
  }
}