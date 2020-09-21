using InventoryTracking.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventoryTracking.Persistence
{
    /// <summary>
    /// Provides method to work with entities.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Db context
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// Repo ctor
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Marks entity as added.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = await Context.Set<TEntity>().AddAsync(entity);
            return addedEntity.Entity;
        }

        /// <summary>
        /// Marks entities as added.
        /// </summary>
        /// <param name="entities">Added entities.</param>
        public void Add(List<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            return;
        }

        /// <summary>
        /// Attach entity to context and mark as modified to perform update on commit.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="forceEntityAttach">Determine whether we nead to override attached entity on saving.</param>
        public TEntity Update(TEntity entity, bool forceEntityAttach = true)
        {
            var attachedEntity = forceEntityAttach ? Attach(entity) : entity;
            Context.Entry(entity).State = EntityState.Modified;
            return attachedEntity;
        }

        /// <summary>
        /// Marks entity as deleted.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            var attachedEntity = Attach(entity);
            Context.Set<TEntity>().Remove(attachedEntity);
        }

        /// <summary>
        /// Detects whether entity has been modified.
        /// </summary>
        /// <param name="entity">Entity to check.</param>
        /// <returns>True if entity has been modified, False if entity is unchanged.</returns>
        public bool IsChanged(TEntity entity)
        {
            var entry = Context.Entry(entity);
            return entry != null && entry.State != EntityState.Unchanged;
        }

        /// <summary>
        /// Safety attaches entity to context by detaching logical entity duplicates, if any, first.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private TEntity Attach(TEntity entity)
        {
            var logicalDuplicate = Context.ChangeTracker.Entries<TEntity>().FirstOrDefault(e => e.Entity.Id == entity.Id);
            if (logicalDuplicate != null)
            {
                Context.Entry(logicalDuplicate.Entity).State = EntityState.Detached;
            }
            return Context.Set<TEntity>().Attach(entity).Entity;
        }

        /// <summary>
        /// Returns <see cref="IQueryable{TEntity}" /> from underlying provider with tracking ability set and includes specified.
        /// </summary>
        /// <returns>Queryable result with specified includes.</returns>
        public IQueryable<TEntity> Get(bool withTracking = false, params Expression<Func<TEntity, object>>[] includes)
        {
            return Get(withTracking).IncludeMultiple(includes);
        }
        /// <summary>
        /// Returns <see cref="IQueryable{TEntity}" /> from underlying provider with tracking ability set.
        /// </summary>
        /// <returns>Queryable result with specified includes.</returns>
        public IQueryable<TEntity> Get(bool withTracking = false)
        {
            var query = Context.Set<TEntity>().AsQueryable();
            if (!withTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public async Task<IList<TEntity>> GetList(params Expression<Func<TEntity, object>>[] includes)
        {
            return await Context.Set<TEntity>().IncludeMultiple(includes).ToListAsync();
        }
    }
}
