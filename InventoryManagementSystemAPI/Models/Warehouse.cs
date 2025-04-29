namespace InventoryManagementSystemAPI.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }
        public List<Products> Products { get; set; } = new List<Products>();
        public List<InventoryTransactions> OutgoingInventoryTransactions { get; set; } = new List<InventoryTransactions>();
        public List<InventoryTransactions> IncomingInventoryTransactions { get; set; } = new List<InventoryTransactions>();
    }
}
