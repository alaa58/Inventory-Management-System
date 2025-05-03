using AutoMapper;
using InventoryManagementSystemAPI.Data;
using InventoryManagementSystemAPI.DTO.InventoryTransactionDTO;
using InventoryManagementSystemAPI.Models;
using InventoryManagementSystemAPI.Services;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Commands.InventoryTransactionCommand
{
    public class TransferInventoryTransactionCommand : IRequest<TransferInventoryTransactionDTO>
    {
        public TransferInventoryTransactionDTO transferInventoryTransactionDTO;

        public TransferInventoryTransactionCommand(TransferInventoryTransactionDTO transferInventoryTransactionDTO)
        {
           this.transferInventoryTransactionDTO = transferInventoryTransactionDTO;
        }
    }
    public class TransferInventoryTransactionCommandHandler : IRequestHandler<TransferInventoryTransactionCommand, TransferInventoryTransactionDTO>
    {
        private readonly IGeneralRepository<InventoryTransaction> repository;
        private readonly IMapper mapper;
        public TransferInventoryTransactionCommandHandler(IGeneralRepository<InventoryTransaction> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<TransferInventoryTransactionDTO> Handle(TransferInventoryTransactionCommand request, CancellationToken cancellationToken)
        {
            InventoryTransaction inventoryTransaction = mapper.Map<InventoryTransaction>(request.transferInventoryTransactionDTO);
            repository.Add(inventoryTransaction);
            await repository.SaveChangesAsync();
            TransferInventoryTransactionDTO transferInventoryTransactionDTO = mapper.Map<TransferInventoryTransactionDTO>(inventoryTransaction);
            return (transferInventoryTransactionDTO);
        }
    }
}
