using DigitalStore.Base.Entity;
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
    public class UnitOfWork<T> : IDisposable, IUnitOfWork<T> where T : BaseEntity
    {
        private readonly StoreIdentityDbContext dbContext;
        public IGenericRepository<T> GenericRepository { get; }
        public UnitOfWork(StoreIdentityDbContext dbContext)
        {
            this.dbContext = dbContext;
            GenericRepository = new GenericRepository<T>(dbContext);
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
