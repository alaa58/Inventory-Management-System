using InventoryManagementSystemAPI.CQRS.Commands.InventoryCommands;
using InventoryManagementSystemAPI.CQRS.Commands.InventoryTransactionCommand;
using InventoryManagementSystemAPI.DTO.InventoryDTO;
using InventoryManagementSystemAPI.DTO.InventoryTransactionDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;

namespace InventoryManagementSystemAPI.CQRS.Orchestrators
{
    public class TransferStockOrchestrator : IRequest
    {
        public TransferStockDTO transferStockDTO { get; }

        public TransferStockOrchestrator(TransferStockDTO dto)
        {
            transferStockDTO = dto;
        }
    }

    public class TransferStockOrchestratorHandler : IRequestHandler<TransferStockOrchestrator>
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransferStockOrchestratorHandler(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(TransferStockOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.transferStockDTO;

            var userId = _httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            await _mediator.Send(new TransferStockCommand(dto.Quantity, dto.FromWarehouseId, dto.ToWarehouseId, dto.ProductId));

            var transactionDto = new TransferInventoryTransactionDTO
            {
                ProductId = dto.ProductId,
                fromWarehouseId = dto.FromWarehouseId,
                toWarehouseId = dto.ToWarehouseId,
                Quantity = dto.Quantity,
                TransactionType = TransactionType.Transfer,
                UserId = userId,
            };

            await _mediator.Send(new TransferInventoryTransactionCommand(transactionDto));

            return Unit.Value;
        }
    }
}
