using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.DTO.InventoryTransactionDTO
{
    public class TransferInventoryTransactionDTO
    {
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ProductId { get; set; }
        public int toWarehouseId { get; set; }
        public int fromWarehouseId { get; set; }
        public string? UserId { get; set; }
    }
}
