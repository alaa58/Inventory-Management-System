using AutoMapper;
using Hangfire;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Commands.InventoryCommands
{
    public class TransferStockCommand : IRequest
    {
        public int Quantity { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public int ProductId { get; set; }

        public TransferStockCommand(int quantity, int fromWarehouseId, int toWarehouseId, int productId)
        {
            Quantity = quantity;
            FromWarehouseId = fromWarehouseId;
            ToWarehouseId = toWarehouseId;
            ProductId = productId;
        }
    }

    public class TransferStockCommandHandler : IRequestHandler<TransferStockCommand>
    {
        private readonly IGeneralRepository<Inventory> repository;

        public TransferStockCommandHandler(IGeneralRepository<Inventory> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(TransferStockCommand request, CancellationToken cancellationToken)
        {
            var fromInventory = repository.Get(i => i.ProductId == request.ProductId &&
                                                    i.WarehouseId == request.FromWarehouseId)
                                          .FirstOrDefault();

            var toInventory = repository.Get(i => i.ProductId == request.ProductId &&
                                                  i.WarehouseId == request.ToWarehouseId)
                                        .FirstOrDefault();

            if (fromInventory == null)
            {
                throw new Exception("Product not found in source warehouse.");
            }

            else if (toInventory == null)
            {
                throw new Exception("Product not found in target warehouse.");
            }
            else if (fromInventory.Quantity < request.Quantity)
            {
                throw new Exception("Not enough stock in source warehouse.");
            }
            fromInventory.Quantity -= request.Quantity;
            toInventory.Quantity += request.Quantity;
            
            if(fromInventory.Quantity < fromInventory.LowStockThreshold)
            {
                BackgroundJob.Enqueue(() =>
                    Console.WriteLine($"Quantity of productID {fromInventory.ProductId} is below LowStockThreshold: {fromInventory.LowStockThreshold}")
                );
            }

            repository.Update(fromInventory);
            repository.Update(toInventory);
            await repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
