using InventoryManagementSystemAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.DTO.InventoryTransactionDTO
{
    public class InventoryTransactionRemoveStockDTO
    {
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ProductId { get; set; }
        public int toWarehouseId { get; set; }
        public string? UserId { get; set; }
    }
}
