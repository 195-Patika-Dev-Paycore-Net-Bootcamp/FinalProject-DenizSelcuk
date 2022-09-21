using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
