using System.ComponentModel.DataAnnotations;

namespace InventoryTracking.Entities.BaseEntities
{
    /// <summary>
    /// Entity which supports read operations on it
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Entity's identifier. Virtual to support Entity mocking in unit tests.
        /// </summary>
        [Key]
        public virtual int Id { get; set; }
    }
}
