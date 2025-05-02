using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.ProductDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Commands.ProductCommands
{
    public class RemoveProductCommand : IRequest
    {
        public int Id { get; set; }

        public RemoveProductCommand(int id)
        {
            Id = id;
        }

    }
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand>
    {
        private readonly IGeneralRepository<Product> repository;
        private readonly IMapper mapper;

        public RemoveProductCommandHandler(IGeneralRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = repository.Get(p => p.ID == request.Id).FirstOrDefault();
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.IsDeleted = true;
            repository.Update(product);
            await repository.SaveChangesAsync();
            return (Unit.Value);
        }
    }
}
