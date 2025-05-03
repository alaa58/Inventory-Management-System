using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.Product;
using InventoryManagementSystemAPI.Interfaces;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystemAPI.CQRS.Commands.ProductCommands
{
    public class UpdateProductCommand : IRequest<UpdateProductDTO>
    {
        public UpdateProductDTO ProductDTO { get; set; }
        public int Id { get; set; }

        public UpdateProductCommand(int id, UpdateProductDTO productDTO)
        {
            this.Id = id;
            this.ProductDTO = productDTO;
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductDTO>
    {
        private readonly IGeneralRepository<Product> repository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IGeneralRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<UpdateProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await repository.Get(p => p.ID == request.Id).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = request.ProductDTO.Name;
            product.Price = request.ProductDTO.Price;
            product.Description = request.ProductDTO.Description;

            repository.Update(product);

            try
            {
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                throw;
            }

            var updatedProductDTO = mapper.Map<UpdateProductDTO>(product);

            return updatedProductDTO;
        }
    }
}
