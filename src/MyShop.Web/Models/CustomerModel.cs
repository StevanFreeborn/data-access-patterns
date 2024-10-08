﻿namespace MyShop.Web.Models;

public class CustomerModel
{
  public string Name { get; set; } = string.Empty;
  public string ShippingAddress { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string PostalCode { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
}