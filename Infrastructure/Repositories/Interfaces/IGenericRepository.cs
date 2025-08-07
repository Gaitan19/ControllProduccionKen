using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    //



    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include);
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity> GetByIdIncludeAsync(Expression<Func<TEntity, bool>> predicate,
                                  params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Agrega en bloque varias entidades al contexto (no guarda cambios).
        /// </summary>
        Task BulkInsertAsync(IEnumerable<TEntity> entities);
        Task BulkUpdateAsync(IEnumerable<TEntity> entities);
        Task BulkInsertAndSaveAsync(IEnumerable<TEntity> entities);
    }
}
