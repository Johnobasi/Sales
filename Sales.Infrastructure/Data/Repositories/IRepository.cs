using Microsoft.EntityFrameworkCore;
using Sales.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        DbSet<TEntity> Entities { get; }

        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }

        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<bool> SaveAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null);
    }
}