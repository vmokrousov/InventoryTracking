using InventoryTracking.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracking.Service.Services
{
    /// <summary>
    /// Generic service provides basic read operations to work with entity.
    /// </summary>
    public class ReadableEntityService<TEntity> : IReadableEntityService<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Entity selector to perform data reading operations.
        /// </summary>
        protected readonly ISelector<TEntity> Selector;

        /// <summary>
        /// Entity service constructor.
        /// </summary>
        /// <param name="selector">Provides operations to select information from storage without it's state modification.</param>
        public ReadableEntityService(ISelector<TEntity> selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">Entity Id.</param>
        /// <param name="withTracking">Flag defining if tracking should be turned on for selected entity.</param>
        /// <returns>Entity with specified Id.</returns>
        public virtual Task<TEntity> GetById(int id, bool withTracking = false)
        {
            return Selector.Get(withTracking).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Gets list of entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        public virtual Task<List<TEntity>> GetAll()
        {
            return Selector.Get().OrderBy(e => e.Id).ToListAsync();
        }
    }
}
