using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.InventoryTransactionDTO;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Commands.InventoryTransactionCommand
{
    public class InventoryTransactionAddStockCommand: IRequest<InventoryTransactionAddStockDTO>
    {
        public InventoryTransactionAddStockDTO inventoryTransactionAddStockDTO;

        public InventoryTransactionAddStockCommand(InventoryTransactionAddStockDTO inventoryTransactionAddStockDTO)
        {
           this.inventoryTransactionAddStockDTO = inventoryTransactionAddStockDTO;
        }
    }
    public class InventoryTransactionAddStockCommandHandler : IRequestHandler<InventoryTransactionAddStockCommand, InventoryTransactionAddStockDTO>
    {
        private readonly IGeneralRepository<InventoryTransaction> repository;
        private readonly IMapper mapper;
        public InventoryTransactionAddStockCommandHandler(IGeneralRepository<InventoryTransaction> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<InventoryTransactionAddStockDTO> Handle(InventoryTransactionAddStockCommand request, CancellationToken cancellationToken)
        {
            InventoryTransaction inventoryTransaction = mapper.Map<InventoryTransaction>(request.inventoryTransactionAddStockDTO);
            repository.Add(inventoryTransaction);
            await repository.SaveChangesAsync();
            InventoryTransactionAddStockDTO inventoryTransactionAddStockDTO = mapper.Map<InventoryTransactionAddStockDTO>(inventoryTransaction);
            return (inventoryTransactionAddStockDTO);
        }
    }
}
