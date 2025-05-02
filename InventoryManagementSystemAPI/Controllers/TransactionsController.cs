using InventoryManagementSystemAPI.CQRS.Orchestrators;
using InventoryManagementSystemAPI.DTO.InventoryDTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TransactionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("add-stock")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddStock(AddStockDTO dto)
        {
            var result = await mediator.Send(new AddStockOrchestrator(dto));
            return Ok(result);
        }
        [HttpPost("remove-stock")]
        public async Task<IActionResult> RemoveStock(RemoveStockDTO dto)
        {
            var result = await mediator.Send(new RemoveStockOrchestrator(dto));
            return Ok(result);
        }

    }
}
