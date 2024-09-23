using MyShop.Domain.Models;

namespace MyShop.Infrastructure.Lazy;

public class GhostCustomer : CustomerProxy
{
  private LoadStatus _loadStatus = LoadStatus.Ghost;
  private readonly Func<Customer> _loadDelegate;

  public bool IsGhost => _loadStatus == LoadStatus.Ghost;
  public bool IsLoaded => _loadStatus == LoadStatus.Loaded;

  public GhostCustomer(Func<Customer> loadDelegate)
  {
    _loadDelegate = loadDelegate;
  }

  public void Load()
  {
    if (IsGhost)
    {
      _loadStatus = LoadStatus.Loading;

      var customer = _loadDelegate();
      CustomerId = customer.CustomerId;
      Name = customer.Name;
      City = customer.City;
      PostalCode = customer.PostalCode;
      ShippingAddress = customer.ShippingAddress;
      Country = customer.Country;
      ProfilePictureValueHolder = customer.ProfilePictureValueHolder;

      _loadStatus = LoadStatus.Loaded;
    }
  }

  public override byte[] ProfilePicture
  {
    get
    {
      Load();
      return base.ProfilePicture;
    }

    set
    {
      Load();
      base.ProfilePicture = value;
    }
  }
}

public enum LoadStatus
{
  Ghost,
  Loading,
  Loaded
}