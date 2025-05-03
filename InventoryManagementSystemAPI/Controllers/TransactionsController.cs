using InventoryManagementSystemAPI.CQRS.Orchestrators;
using InventoryManagementSystemAPI.DTO.InventoryDTO;
using InventoryManagementSystemAPI.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystemAPI.DTO.InventoryTransactionDTO;
using InventoryManagementSystemAPI.CQRS.Queries.TransactionQueries;
using InventoryManagementSystemAPI.Models;

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
        try
        {
            var result = await mediator.Send(new AddStockOrchestrator(dto));
            return Ok(ResponseDTO<string>.Succeded(null, "stock added successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ResponseDTO<string>.Error(ErrorCode.UnExcepectedError, $"failed to add stock: {ex.Message}"));
        }
    }

    [HttpPost("remove-stock")]
    public async Task<IActionResult> RemoveStock(RemoveStockDTO dto)
    {
        try
        {
            var result = await mediator.Send(new RemoveStockOrchestrator(dto));
            return Ok(ResponseDTO<string>.Succeded(null, "Stock removed successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ResponseDTO<string>.Error(ErrorCode.UnExcepectedError, $"failed to remove stock: {ex.Message}"));
        }
    }

    [HttpPost("transfer-stock")]
    public async Task<IActionResult> TransferStock(TransferStockDTO dto)
    {
        try
        {
            await mediator.Send(new TransferStockOrchestrator(dto));
            return Ok(ResponseDTO<string>.Succeded(null, "stock transferred successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ResponseDTO<string>.Error(ErrorCode.UnExcepectedError, $"failed to transfer stock: {ex.Message}"));
        }

    }
    [HttpGet("get-transactions")]
    public async Task<IActionResult> GetTransactions([FromQuery] int productId, [FromQuery] TransactionType transactionType)
    {
        try
        {
            var result = await mediator.Send(new GetTransactionByProductIdQuery(productId, transactionType));
            return Ok(ResponseDTO<List<TransactionHistoryDTO>>.Succeded(result.ToList(), "transactions retrieved successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                ResponseDTO<string>.Error(ErrorCode.UnExcepectedError, $"failed to retrieve transactions: {ex.Message}"));
        }
    }

}