using DigitalStore.Data.Domain;
using DigitalStore.Data.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.UnitOfWork
{
    public interface IUnitOfWork<T> where T : class
    {
        Task Complete();
        Task CompleteWithTransaction();
        IGenericRepository<T> GenericRepository { get; }
    }
}
