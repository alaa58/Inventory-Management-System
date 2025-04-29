using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; } = new Warehouse();
        public List<InventoryTransactions> inventoryTransactions { get; set; } = new List<InventoryTransactions>();
    }
}
