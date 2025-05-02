using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.DTO.Product
{
    public class AddProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
