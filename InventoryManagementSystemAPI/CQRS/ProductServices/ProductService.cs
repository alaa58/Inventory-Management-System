using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.Product;
using InventoryManagementSystemAPI.Interfaces;
using InventoryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryManagementSystemAPI.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IGeneralRepository<Product> ProductRepo;
        private readonly IMapper mapper;

        public ProductService(IGeneralRepository<Product> ProductRepo, IMapper mapper)
        {
            this.ProductRepo = ProductRepo;
            this.mapper = mapper;
        }
        public async Task<AddProductDTO> AddProduct(AddProductDTO productDTO)
        {
            //Product product = new Product
            //{
            //    Name = productDTO.Name,
            //    Description = productDTO.Description,
            //    Price = productDTO.Price
            //};
            //ProductRepo.Add(product);
            //ProductRepo.Save();

            //return product;
            var product = mapper.Map<Product>(productDTO);
            ProductRepo.Add(product);
            //ProductRepo.Save();
            return productDTO;
        }
        public async Task<Product> UpdateProduct(int id, UpdateProductDTO productDTO)
        {
            var product = ProductRepo.Get(p => p.ID == id).FirstOrDefault();
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;

            //ProductRepo.Update(product);
            //ProductRepo.Save();
            return product;
        }
    }
}
