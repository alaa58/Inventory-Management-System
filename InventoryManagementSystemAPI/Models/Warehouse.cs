namespace InventoryManagementSystemAPI.Models
{
    public class Warehouse : BaseModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Inventory>? Inventories { get; set; }
        public List<InventoryTransaction>? OutgoingInventoryTransactions { get; set; }
        public List<InventoryTransaction>? IncomingInventoryTransactions { get; set; }
    }
}
