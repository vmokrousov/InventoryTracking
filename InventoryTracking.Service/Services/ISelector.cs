using System;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryTracking.Service.Services
{
    /// <summary>
    /// An interface to query the data.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface ISelector<TEntity>
        where TEntity : class
    {
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
    }
}
