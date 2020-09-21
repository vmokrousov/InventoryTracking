using InventoryTracking.Entities.BaseEntities;
using InventoryTracking.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracking.Service.Services
{
    /// <summary>
    /// Generic service provides basic CRUD operations to work with entity.
    /// </summary>
    public class EntityService<TEntity> : ReadableEntityService<TEntity>, IEntityService<TEntity> where TEntity : Entity
    {
       

        /// <summary>
        /// Entity repository to perform data modification operations.
        /// </summary>
        protected readonly IRepository<TEntity> Repository;

        /// <summary>
        /// Unit of work to commit changes in data.
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Entity service constructor.
        /// </summary>
        /// <param name="repository">Provides operation which modifies state of parmenent storage.</param>
        /// <param name="selector">Provides operations to select information from storage without it's state modification.</param>
        /// <param name="unitOfWork">Commit changes to permanent storage made throught repository.</param>
        public EntityService(ISelector<TEntity> selector, IRepository<TEntity> repository, IUnitOfWork unitOfWork)
            : base(selector)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds entities to permanent storage entity collection.
        /// </summary>
        /// <param name="entities">Entities to be added.</param>
        public virtual async Task<List<TEntity>> Add(List<TEntity> entities)
        {
            Repository.Add(entities);
            await UnitOfWork.CommitAsync();
            return entities;
        }

        /// <summary>
        /// Updates entity information in permanent storage.
        /// </summary>
        /// <param name="entity">Updated entity.</param>
        public virtual async Task<TEntity> Update(TEntity entity)
        {
            TEntity existingEntity = await GetById(entity.Id, true);
            if (existingEntity == null)
            {
                //throw new EntityGoneException(entity.Id, typeof(TEntity).Name);
                throw new Exception(string.Format("Requested {0} with id = {1} has been deleted or doesn't exist.", typeof(TEntity).Name, entity.Id));
            }

            return await Update(entity, existingEntity);
        }

        /// <summary>
        /// When overriden in child class updates fields in entityToUpdate to previous values to prevent from changing fields.
        /// </summary>
        /// <param name="entityToUpdate">Updated entity, will be attached to context.</param>
        /// <param name="updates">Entity updates.</param>
        public virtual async Task<TEntity> Update(TEntity entityToUpdate, TEntity updates)
        {
            UpdateChildren(updates, entityToUpdate);

            TEntity updatedEntity = Repository.Update(entityToUpdate);

            await UnitOfWork.CommitAsync();
            return updatedEntity;
        }

        /// <summary>
        /// Updates entity information in permanent storage.
        /// </summary>
        /// <param name="entity">Updated entity.</param>
        public virtual async Task<TEntity> UpdateAttached(TEntity entity)
        {
            TEntity updatedEntity = Repository.Update(entity, forceEntityAttach: false);

            await UnitOfWork.CommitAsync();
            return updatedEntity;
        }

        /// <summary>
        /// When overriden in child class updates fields in entityToUpdate to previous values to prevent from changing fields.
        /// </summary>
        /// <param name="entityToUpdate">Updated entity, will be attached to context.</param>
        /// <param name="updates">Entity updates.</param>
        public virtual async Task<TEntity> UpdateAttached(TEntity entityToUpdate, TEntity updates)
        {
            SyncChanges(entityToUpdate, updates);
            return await UpdateAttached(entityToUpdate);
        }

        /// <summary>
        /// Updates state of enitity children by marking them eather for removal, update or creation.
        /// </summary>
        /// <param name="existingEntity"></param>
        /// <param name="updatedEntity"></param>
        public virtual void UpdateChildren(TEntity existingEntity, TEntity updatedEntity) { }

        /// <summary>
        /// Synchronizes changes between entity to update and received entity updates.
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="updates"></param>
        public virtual void SyncChanges(TEntity entityToUpdate, TEntity updates) { }

        /// <summary>
        /// Deletes entity from permanent storage entity collection.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        public async Task Delete(TEntity entity)
        {
            Repository.Delete(entity);

            await UnitOfWork.CommitAsync();
            //TODO: Implement and test for correct work.
            //Repository.Delete(entity);
            //return UnitOfWork.CommitAsync();
        }
    }
}
