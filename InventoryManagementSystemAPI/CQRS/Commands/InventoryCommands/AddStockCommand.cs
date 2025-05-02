using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.InventoryDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Commands.InventoryCommands
{
    public class AddStockCommand : IRequest<AddStockDTO>
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public AddStockCommand(int Quantity, int ProductId, int WarehouseId)
        {
            this.Quantity = Quantity;
            this.ProductId = ProductId;
            this.WarehouseId = WarehouseId;
        }
    }
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, AddStockDTO>
    {
        private IGeneralRepository<Inventory> repository;
        private IMapper mapper;

        public AddStockCommandHandler(IGeneralRepository<Inventory> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<AddStockDTO> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            Inventory inventory = repository.Get(i => i.ProductId == request.ProductId && i.WarehouseId == request.WarehouseId).FirstOrDefault();

            if (inventory == null)
            {
                throw new Exception("Product not found");
            }

            inventory.Quantity += request.Quantity;
            repository.Update(inventory);
            await repository.SaveChangesAsync();
            var updatedInventoryDTO = mapper.Map<AddStockDTO>(inventory);
            return (updatedInventoryDTO);
        }
    }
}
