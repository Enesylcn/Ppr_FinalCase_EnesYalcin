using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DigitalStore.Data.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Save();
        Task<TEntity?> GetById(long Id, params string[] includes);
        Task<TEntity> Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task Delete(long Id);
        Task<List<TEntity>> GetAll(params string[] includes);
        Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression, params string[] includes);
        Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> expression, params string[] includes);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);
    }
}
