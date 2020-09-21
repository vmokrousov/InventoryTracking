using InventoryTracking.Entities;
using InventoryTracking.Persistence;
using InventoryTracking.Service.Dto;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventoryTracking.Service.Services
{

    public class InventoryService : EntityService<Inventory>, IInventoryService
    {
        // <summary>
        /// Service constructor
        /// </summary>
        /// <param name="selector"></param>
        public InventoryService(ISelector<Inventory> selector, IUnitOfWork unitOfWork,  IRepository<Inventory> repository) : base(selector, repository, unitOfWork)
        {
        }

        /// <summary>
        /// Add new inventories
        /// </summary>
        /// <param name="inventories"></param>
        /// <returns></returns>
        public async Task<List<Inventory>> Save(List<Inventory> inventories)
        {
            return await Add(inventories);
        }

        /// <summary>
        /// Get List of inventories
        /// </summary>
        /// <returns></returns>
        public async Task<List<Inventory>> GetList()
        {
            return await GetAll();
        }

        public async Task<Inventory> GetByName(string itemname)
        {
            return await Selector.Get(false)
                .Where(x => x.Name == itemname).FirstOrDefaultAsync();
        }

        public Inventory GetById(int Id)
        {
            return Selector.Get(false)
                .Where(x => x.Id == Id).FirstOrDefault();
        }

        /// <summary>
        /// Updates Chart
        /// </summary>
        /// <param name="chart"></param>
        /// <returns></returns>
        public async Task<Inventory> UpdateInventory(Inventory entityToUpdate)
        {
            Inventory updates = GetById(entityToUpdate.Id);
            return await base.Update(entityToUpdate, updates);
        }

        public async Task DeleteInventory(string itemname)
        {
            Inventory entity = await GetByName(itemname);
            await Delete(entity);
        }

        public async Task BulkDeleteInventory(List<string> itemnames)
        {
            foreach (var item in itemnames)
            {
                Inventory entity = await GetByName(item);
                await Delete(entity);
            }
        }

        public async Task<List<Inventory>> Search(InventoryFilter request)
        {
            var filter = Filter(request);

            IQueryable<Inventory> inventoryQuery = Selector.Get();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var lowerSearchPattern = request.Name.Trim().ToLower();

                inventoryQuery = inventoryQuery.Where(x =>
                    x.Name != null && x.Name.ToLower().Contains(lowerSearchPattern));
            }

            if (request.GetHighestQuantity)
            {
                var maxValue = inventoryQuery.Max(x => x.Quantity);
                inventoryQuery = inventoryQuery.Where(x => x.Quantity == maxValue);
            }

            if (request.GetLowestQuantity) 
            {
                int minValue = inventoryQuery.Min(x => x.Quantity);
                inventoryQuery = inventoryQuery.Where(x => x.Quantity == minValue);
            }

            if (request.GetOldestItem)
            {
                var oldestDate = inventoryQuery.OrderBy(x => x.CreatedOn).First(); // this will give oldest date
                inventoryQuery = inventoryQuery.Where(x => x.CreatedOn == oldestDate.CreatedOn);
            }

            if (request.GetNewestItem)
            {
                var latestDate = inventoryQuery.OrderByDescending(x => x.CreatedOn).First(); // this will give latest date
                inventoryQuery = inventoryQuery.Where(x => x.CreatedOn == latestDate.CreatedOn);
            }

            return await inventoryQuery.ToListAsync();

        }

        /// <summary>
        /// TODO: Add more filters if needed
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private Expression<Func<Inventory, bool>> Filter(InventoryFilter filter)
        {
            Expression<Func<Inventory, bool>> predicate = j => j.Name != null;

            if (filter == null)
            {
                return predicate;
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicate = predicate.And(x => x.Name.Contains(filter.Name));
            }

           
            return predicate;
        }
    }
}
