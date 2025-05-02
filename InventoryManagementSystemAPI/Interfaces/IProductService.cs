using InventoryManagementSystemAPI.DTO.Product;
using InventoryManagementSystemAPI.Models;

namespace InventoryManagementSystemAPI.Interfaces
{
    public interface IProductService 
    {
        public Task<AddProductDTO> AddProduct(AddProductDTO productDTO);
        public Task<Product> UpdateProduct(int id, UpdateProductDTO productDTO);
    }
}
