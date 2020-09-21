

using InventoryTracking.Entities;
using InventoryTracking.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTracking.Service.Services
{
    public interface IInventoryService
    {
        /// <summary>
        /// Add new inventories
        /// </summary>
        /// <param name="inventories"></param>
        /// <returns></returns>
        Task<List<Inventory>> Save(List<Inventory> inventories);

        /// <summary>
        /// Get List of inventories
        /// </summary>
        /// <returns></returns>
        Task<List<Inventory>> GetList();

        Inventory GetById(int Id);

        /// <summary>
        /// Updates Chart
        /// </summary>
        /// <param name="chart"></param>
        /// <returns></returns>
        Task<Inventory> UpdateInventory(Inventory entityToUpdate);
        Task<Inventory> GetByName(string itemname);
        Task DeleteInventory(string itemname);
        Task BulkDeleteInventory(List<string> itemnames);
        Task<List<Inventory>> Search(InventoryFilter request);
    }
}
