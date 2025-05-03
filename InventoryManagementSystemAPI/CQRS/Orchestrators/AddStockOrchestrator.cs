using MediatR;
using InventoryManagementSystemAPI.CQRS.Commands.InventoryCommands;
using InventoryManagementSystemAPI.CQRS.Commands.InventoryTransactionCommand;
using InventoryManagementSystemAPI.DTO.InventoryDTO;
using InventoryManagementSystemAPI.DTO.InventoryTransactionDTO;

namespace InventoryManagementSystemAPI.CQRS.Orchestrators
{
    public class AddStockOrchestrator : IRequest<AddStockDTO>
    {
        public AddStockDTO AddStockDTO { get; }

        public AddStockOrchestrator(AddStockDTO dto)
        {
            AddStockDTO = dto;
        }
    }


    public class AddStockOrchestratorHandler : IRequestHandler<AddStockOrchestrator, AddStockDTO>
    {
        private readonly IMediator _mediator;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddStockOrchestratorHandler(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AddStockDTO> Handle(AddStockOrchestrator request, CancellationToken cancellationToken)
        {
            var dto = request.AddStockDTO;
            var userId = _httpContextAccessor.HttpContext?.User?
           .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            var inventoryResult = await _mediator.Send(new AddStockCommand(dto.Quantity, dto.ProductId, dto.WarehouseId));

            var transactionDto = new InventoryTransactionAddStockDTO
            {
                ProductId = dto.ProductId,
                toWarehouseId = dto.WarehouseId,
                Quantity = dto.Quantity,
                TransactionType = Models.TransactionType.Add,
                UserId = userId,
            };

            await _mediator.Send(new InventoryTransactionAddStockCommand(transactionDto));

            return inventoryResult;
        }
    }

}
