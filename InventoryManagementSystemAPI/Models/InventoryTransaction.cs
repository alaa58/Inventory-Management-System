using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.Models
{
    public class InventoryTransaction: BaseModel
    {
        public TransactionType TransactionType { get; set; } 
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
       
        [ForeignKey("FromWarehouse")]
        [InverseProperty("OutgoingInventoryTransactions")]
        public int? FromWarehouseId { get; set; }
        [ForeignKey("ToWarehouse")]
        [InverseProperty("IncomingInventoryTransactions")]
        public int? ToWarehouseId { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Warehouse? FromWarehouse { get; set; } 
        public virtual Warehouse? ToWarehouse { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
    public enum TransactionType
    {
        Add,
        Remove,
        Transfer
    }
}
