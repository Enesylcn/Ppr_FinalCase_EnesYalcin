using DigitalStore.Base;
using DigitalStore.Data.Context;
using DigitalStore.Data.Domain;
using DigitalStore.Data.GenericRepository;

namespace DigitalStore.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StoreIdentityDbContext dbContext;
        private readonly SessionContext sessionContext;

        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        public IGenericRepository<ProductCategory> ProductCategoryRepository { get; }
        public IGenericRepository<ShoppingCart> ShoppingCartRepository { get; }
        public IGenericRepository<ShoppingCartItem> ShoppingCartItemRepository { get; }

        public UnitOfWork(StoreIdentityDbContext dbContext, SessionContext sessionContext)
        {
            this.dbContext = dbContext;
            this.sessionContext = sessionContext;

            CategoryRepository = new GenericRepository<Category>(this.dbContext, this.sessionContext);
            ProductRepository = new GenericRepository<Product>(this.dbContext, this.sessionContext);
            OrderRepository = new GenericRepository<Order>(this.dbContext, this.sessionContext);
            OrderDetailRepository = new GenericRepository<OrderDetail>(this.dbContext, this.sessionContext);
            ProductCategoryRepository = new GenericRepository<ProductCategory>(this.dbContext, this.sessionContext);
            ShoppingCartRepository = new GenericRepository<ShoppingCart>(this.dbContext, this.sessionContext);
            ShoppingCartItemRepository = new GenericRepository<ShoppingCartItem>(this.dbContext, this.sessionContext);
        }

        public void Dispose()
        {
        }

        public async Task Complete()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task CompleteWithTransaction()
        {
            using (var dbTransaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await dbContext.SaveChangesAsync();
                    await dbTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbTransaction.RollbackAsync();
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }
    }
}
