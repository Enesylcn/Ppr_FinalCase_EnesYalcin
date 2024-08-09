using DigitalStore.Data.Domain;
using DigitalStore.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Complete();
        Task CompleteWithTransaction();
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        IGenericRepository<ProductCategory> ProductCategoryRepository { get; }
        IGenericRepository<ShoppingCart> ShoppingCartRepository { get; }
        IGenericRepository<ShoppingCartItem> ShoppingCartItemRepository { get; }
    }
}
