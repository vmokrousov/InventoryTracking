

namespace InventoryTracking.Service.Dto
{
    public class InventoryFilter
    {
        public string Name { get; set; }
        public bool GetHighestQuantity { get; set; }
        /// <summary>
        /// User queries the inventory for the Highest quantity item
        /// </summary>
        public bool GetLowestQuantity { get; set; }
        public bool GetOldestItem { get; set; }
        public bool GetNewestItem { get; set; }
    }
}
