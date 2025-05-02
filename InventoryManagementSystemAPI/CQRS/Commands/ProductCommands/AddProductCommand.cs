using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.Product;
using InventoryManagementSystemAPI.Interfaces;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Commands.ProductCommands
{
    public class AddProductCommand : IRequest<AddProductDTO>
    {
        public AddProductDTO AddProductDTO { get; set; }
        public AddProductCommand(AddProductDTO addProductDTO)
        {
            AddProductDTO = addProductDTO;
        }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductDTO>
    {
        private readonly IGeneralRepository<Product> repository;
        private readonly IMapper mapper;

        public AddProductCommandHandler(IGeneralRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<AddProductDTO> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request.AddProductDTO);
            repository.Add(product);
            await repository.SaveChangesAsync();
            var addedProductDTO = mapper.Map<AddProductDTO>(product);

            return (addedProductDTO);
        }

    }
}

