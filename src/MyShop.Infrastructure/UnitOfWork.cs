using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;

namespace MyShop.Infrastructure;

public class UnitOfWork(ShoppingContext context) : IUnitOfWork
{
  private readonly ShoppingContext _context = context;

  private IRepository<Product>? _productRepository;
  public IRepository<Product> ProductRepository
  {
    get
    {
      _productRepository ??= new ProductRepository(_context);
      return _productRepository;
    }
  }

  private IRepository<Customer>? _customerRepository;
  public IRepository<Customer> CustomerRepository
  {
    get
    {
      _customerRepository ??= new CustomerRepository(_context);
      return _customerRepository;
    }
  }

  private IRepository<Order>? _orderRepository;
  public IRepository<Order> OrderRepository
  {
    get
    {
      _orderRepository ??= new OrderRepository(_context);
      return _orderRepository;
    }
  }

  public async Task SaveChangesAsync()
  {
    await _context.SaveChangesAsync();
  }
}