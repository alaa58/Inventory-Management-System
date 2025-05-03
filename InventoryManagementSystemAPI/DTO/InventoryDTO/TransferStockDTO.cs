namespace InventoryManagementSystemAPI.DTO.InventoryDTO
{
    public class TransferStockDTO
    {
        public int Quantity { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public int ProductId { get; set; }
    }
}
