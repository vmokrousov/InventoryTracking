using InventoryTracking.Entities.BaseEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTracking.Service.Services
{
    /// <summary>
    /// Generic inerface defines basic CRUD operations to work with entity.
    /// </summary>
    public interface IEntityService<TEntity> : IReadableEntityService<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Adds entities to permanent storage entity collection.
        /// </summary>
        /// <param name="entities">Entities to be added.</param>
        Task<List<TEntity>> Add(List<TEntity> entities);

        /// <summary>
        /// Updates entity information in permanent storage.
        /// </summary>
        /// <param name="entity">Updated entity.</param>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Updates state of enitity children by marking them eather for removal, update or creation.
        /// </summary>
        /// <param name="existingEntity"></param>
        /// <param name="updatedEntity"></param>
        void UpdateChildren(TEntity existingEntity, TEntity updatedEntity);

        /// <summary>
        /// Deletes entity from permanent storage entity collection.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        Task Delete(TEntity entity);
    }
}
