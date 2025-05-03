using MediatR;
using InventoryManagementSystemAPI.DTO.InventoryDTO;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Hangfire;
namespace InventoryManagementSystemAPI.CQRS.Commands.InventoryCommands
{
    public class RemoveStockCommand : IRequest<RemoveStockDTO>
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public RemoveStockCommand(int Quantity, int ProductId, int WarehouseId)
        {
            this.Quantity = Quantity;
            this.ProductId = ProductId;
            this.WarehouseId = WarehouseId;
        }
    }
    public class RemoveStockCommandHandler : IRequestHandler<RemoveStockCommand, RemoveStockDTO>
    {
        private IGeneralRepository<Inventory> repository;
        private IMapper mapper;
        public RemoveStockCommandHandler(IGeneralRepository<Inventory> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<RemoveStockDTO> Handle(RemoveStockCommand request, CancellationToken cancellationToken)
        {
            Inventory? inventory = repository.Get(i => i.ProductId == request.ProductId
            && i.WarehouseId == request.WarehouseId)
                .FirstOrDefault();
            if (inventory == null)
            {
                throw new Exception("Product not found");
            }
            else if (inventory.Quantity < request.Quantity)
            {
                throw new Exception("Not enough stock");
            }
            else
            {
                inventory.Quantity -= request.Quantity;
            }
            if(inventory.Quantity < inventory.LowStockThreshold)
            {
                BackgroundJob.Enqueue(() =>
                  Console.WriteLine($"Quantity of productID {inventory.ProductId} is below LowStockThreshold: {inventory.LowStockThreshold}")
                );
            }
            repository.Update(inventory);
            await repository.SaveChangesAsync();
            var updatedInventoryDTO = mapper.Map<RemoveStockDTO>(inventory);
            return (updatedInventoryDTO);
        }
    }
}
