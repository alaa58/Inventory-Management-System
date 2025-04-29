using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.Models
{
    public class InventoryTransactions
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; } 
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Products Product { get; set; } = new Products();
        [ForeignKey("FromWarehouse")]
        [InverseProperty("OutgoingInventoryTransactions")]
        public int FromWarehouseId { get; set; }
        [ForeignKey("ToWarehouse")]
        [InverseProperty("IncomingInventoryTransactions")]
        public int ToWarehouseId { get; set; }
        public virtual Warehouse FromWarehouse { get; set; } = new Warehouse();
        public virtual Warehouse ToWarehouse { get; set; } = new Warehouse();
    }
    public enum TransactionType
    {
        Add,
        Remove,
        Transfer
    }
}
