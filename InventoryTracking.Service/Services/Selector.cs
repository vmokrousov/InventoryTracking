using InventoryTracking.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryTracking.Service.Services
{
    /// <summary>
    /// An interface to query the data.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public class Selector<TEntity> : ISelector<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Underlying context.
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// Ctor for Selector.
        /// </summary>
        public Selector(DbContext context)
        {
            Context = context;
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
    }
}
