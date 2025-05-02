using MediatR;
using InventoryManagementSystemAPI.CQRS.Commands.InventoryCommands;
using InventoryManagementSystemAPI.CQRS.Commands.InventoryTransactionCommand;
using InventoryManagementSystemAPI.DTO.InventoryDTO;
using InventoryManagementSystemAPI.DTO.InventoryTransactionDTO;

namespace InventoryManagementSystemAPI.CQRS.Orchestrators
{
    public class RemoveStockOrchestrator : IRequest<RemoveStockDTO>
    {
        public RemoveStockDTO RemoveStockDTO { get; }

        public RemoveStockOrchestrator(RemoveStockDTO dto)
        {
            RemoveStockDTO = dto;
        }
    }


    public class RemoveStockOrchestratorHandler : IRequestHandler<RemoveStockOrchestrator, RemoveStockDTO>
    {
        private readonly IMediator _mediator;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoveStockOrchestratorHandler(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RemoveStockDTO> Handle(RemoveStockOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.RemoveStockDTO;
            var userId = _httpContextAccessor.HttpContext?.User?
           .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            // Step 1: Remove to Inventory
            var inventoryResult = await _mediator.Send(new RemoveStockCommand(dto.Quantity, dto.ProductId, dto.WarehouseId));

            // Step 2: Remove Transaction Log
              // Step 2: Add Transaction Log
            var transactionDto = new InventoryTransactionAddStockDTO
            {
                ProductId = dto.ProductId,
                toWarehouseId = dto.WarehouseId,
                Quantity = dto.Quantity,
                TransactionType = Models.TransactionType.Remove,
                UserId = userId,
            };

            await _mediator.Send(new InventoryTransactionAddStockCommand(transactionDto));

            return inventoryResult;
        }
    }

}
