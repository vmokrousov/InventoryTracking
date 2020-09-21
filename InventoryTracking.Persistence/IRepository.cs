using InventoryTracking.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventoryTracking.Persistence
{
    /// <summary>
    /// Interface to instruct the underlying data access implementation about changes being made.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Marks entity as added.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Marks entities as added.
        /// </summary>
        /// <param name="entities">Added entities.</param>
        void Add(List<TEntity> entities);

        /// <summary>
        /// Marks entity as modified.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="forceEntityAttach">Determine whether we nead to override attached entity on saving.</param>
        TEntity Update(TEntity entity, bool forceEntityAttach = true);

        /// <summary>
        /// Marks entity as deleted.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Detects whether entity has been modified.
        /// </summary>
        /// <param name="entity">Entity to check.</param>
        /// <returns>True if entity has been modified, False if entity is unchanged.</returns>
        bool IsChanged(TEntity entity);

        /// <summary>
        /// Returns <see cref="IQueryable{TEntity}" /> from underlying provider with tracking ability set.
        /// </summary>
        /// <returns>Queryable result with specified includes.</returns>
        IQueryable<TEntity> Get(bool withTracking = false);

        /// <summary>
        /// Returns <see cref="IQueryable{TEntity}" /> from underlying provider with tracking ability set and includes specified.
        /// </summary>
        /// <returns>Queryable result with specified includes.</returns>
        IQueryable<TEntity> Get(bool withTracking = false, params Expression<Func<TEntity, object>>[] includes);

        Task<IList<TEntity>> GetList(params Expression<Func<TEntity, object>>[] includes);
    }
}
