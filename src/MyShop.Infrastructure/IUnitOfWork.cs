using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;

namespace MyShop.Infrastructure;

public interface IUnitOfWork
{
  IRepository<Product> ProductRepository { get; }
  IRepository<Customer> CustomerRepository { get; }
  IRepository<Order> OrderRepository { get; }
  Task SaveChangesAsync();
}