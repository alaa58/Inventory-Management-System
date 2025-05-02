using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystemAPI.DTO.InventoryDTO
{
    public class AddStockDTO
    {
        public int Quantity { get; set; }
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
    }
}
