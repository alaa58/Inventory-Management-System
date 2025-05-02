using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.Product;
using InventoryManagementSystemAPI.Interfaces;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Commands.ProductCommands
{
    public class UpdateProductCommand : IRequest<UpdateProductDTO>
    {
        public UpdateProductDTO productDTO;
        public int id;

        public UpdateProductCommand(int id, UpdateProductDTO productDTO)
        {
            this.id = id;
            this.productDTO = productDTO;
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
            var product = repository.Get(p => p.ID == request.id).FirstOrDefault();
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Map<UpdateProductDTO>();
            repository.Update(product);
            await repository.SaveChangesAsync();
            var updatedProductDTO = mapper.Map<UpdateProductDTO>(product);

            return (updatedProductDTO);
        }
    }
}
