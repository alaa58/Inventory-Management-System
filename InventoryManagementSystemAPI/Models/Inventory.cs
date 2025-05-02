using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.Models
{
    public class Inventory : BaseModel
    {
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; } 
        public virtual Warehouse? Warehouse { get; set; }
        public List<InventoryTransaction>? InventoryTransactions { get; set; }

    }
}
