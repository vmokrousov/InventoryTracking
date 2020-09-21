using InventoryTracking.Entities.BaseEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTracking.Service.Services
{
    /// <summary>
    /// Generic inerface defyning basic read operations to work with entity.
    /// </summary>
    public interface IReadableEntityService<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Gets entity by id.
        /// </summary>
        /// <param name="id">Entity Id.</param>
        /// <param name="withTracking">Flag defining if tracking should be turned on for selected entity.</param>
        /// <returns>Entity with specified Id.</returns>
        Task<TEntity> GetById(int id, bool withTracking = false);

        /// <summary>
        /// Gets list of entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        Task<List<TEntity>> GetAll();
    }
}
