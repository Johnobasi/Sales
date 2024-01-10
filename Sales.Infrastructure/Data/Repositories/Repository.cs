using Microsoft.EntityFrameworkCore;
using Sales.Core.Interfaces;
using Sales.Infrastructure.Data.EfCore;
using System.Linq.Expressions;

namespace Sales.Infrastructure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly SalesDbContext DbContext;

        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(SalesDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>();
        }

        #region Async Method

        public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return Entities.FindAsync(ids, cancellationToken);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            //Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            //Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        }

        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            //Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            //Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await DbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> SaveAsync(CancellationToken cancellationToken)
        {
            return await DbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? await DbContext.Set<TEntity>().ToListAsync() : await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? await DbContext.Set<TEntity>().CountAsync() : await DbContext.Set<TEntity>().CountAsync(predicate);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null ? DbContext.Set<TEntity>() : DbContext.Set<TEntity>().Where(predicate);
        }

        #endregion Async Method
    }
}