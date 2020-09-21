using InventoryTracking.Entities.BaseEntities;
using System;

namespace InventoryTracking.Entities
{
    public class Inventory : Entity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
