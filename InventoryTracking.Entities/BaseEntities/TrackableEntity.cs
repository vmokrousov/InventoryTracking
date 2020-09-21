//using System;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace InventoryTracking.Entities.BaseEntities
//{
//    /// <summary>
//    /// Entity which support CRUD operations.
//    /// </summary>
//    public abstract class TrackableEntity : Entity
//    {
//        /// <summary>
//        /// Date the entity was created.
//        /// </summary>
//        public DateTime CreatedDate { get; set; }

//        /// <summary>
//        /// Creator's identifier.
//        /// </summary>
//        [ForeignKey(nameof(CreatedBy))]
//        public int? CreatedByID { get; set; }

//        /// <summary>
//        /// User who created the entity.
//        /// </summary>
//        public virtual User CreatedBy { get; set; }

//        /// <summary>
//        /// Date the entity was modified.
//        /// </summary>
//        public DateTime? ModifiedDate { get; set; }

//        /// <summary>
//        /// Person who modified the entity.
//        /// </summary>
//        public int? ModifiedByID { get; set; }

//        /// <summary>
//        /// Date the entity was deleted.
//        /// </summary>
//        public DateTime? DeletedDate { get; set; }

//        /// <summary>
//        /// Person who deleted the entity.
//        /// </summary>
//        public int? DeletedByID { get; set; }
//    }
//}
