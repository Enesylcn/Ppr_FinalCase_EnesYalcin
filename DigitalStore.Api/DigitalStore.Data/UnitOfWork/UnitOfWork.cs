using DigitalStore.Data.Context;
using DigitalStore.Data.Domain;
using DigitalStore.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StoreIdentityDbContext dbContext;

        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<OrderDetail> OrderDetailRepository { get; }
        public IGenericRepository<ProductCategory> ProductCategoryRepository { get; }

        public UnitOfWork(StoreIdentityDbContext dbContext)
        {
            this.dbContext = dbContext;

            UserRepository = new GenericRepository<User>(this.dbContext);
            CategoryRepository = new GenericRepository<Category>(this.dbContext);
            ProductRepository = new GenericRepository<Product>(this.dbContext);
            OrderRepository = new GenericRepository<Order>(this.dbContext);
            OrderDetailRepository = new GenericRepository<OrderDetail>(this.dbContext);
            ProductCategoryRepository = new GenericRepository<ProductCategory>(this.dbContext);
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
