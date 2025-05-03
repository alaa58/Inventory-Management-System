using InventoryManagementSystemAPI.CQRS.Commands.ProductCommands;
using InventoryManagementSystemAPI.CQRS.Queries.ProductQueries;
using InventoryManagementSystemAPI.DTO;
using InventoryManagementSystemAPI.DTO.Product;
using InventoryManagementSystemAPI.DTO.ProductDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(AddProductCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await mediator.Send(command);
                    return Ok(ResponseDTO<AddProductDTO>.Succeded(result, "product added successfully"));
                }
                return BadRequest(ResponseDTO<AddProductDTO>.Error(ErrorCode.UnExcepectedError,"failed"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO productDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateProductCommand command = new UpdateProductCommand(id, productDTO);
                    UpdateProductDTO result = await mediator.Send(command);

                    return Ok(ResponseDTO<UpdateProductDTO>.Succeded(result, "product updated successfully"));
                }
                return BadRequest(ResponseDTO<UpdateProductDTO>.Error(ErrorCode.UnExcepectedError,"failed"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                var command = new RemoveProductCommand(id);
                var result = await mediator.Send(command);

                return Ok(ResponseDTO<string>.Succeded(null, "Product removed successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseDTO<string>.Error(ErrorCode.UnExcepectedError, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var result = await mediator.Send(new GetAllProductQuery(new AllProductsDTO()));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseDTO<List<AllProductsDTO>>.Error(ErrorCode.UnExcepectedError, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var result = await mediator.Send(new GetProductByIdQuery(id, new GetProductByIdDTO()));
                if (result == null)
                {
                    return NotFound(ResponseDTO<GetProductByIdDTO>.Error(ErrorCode.NotFound, $"product with id {id} not found"));
                }
                return Ok(ResponseDTO<GetProductByIdDTO>.Succeded(result, "product retrieved successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseDTO<GetProductByIdDTO>.Error(ErrorCode.UnExcepectedError, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpGet("GetProductsInTheInventory")]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> GetProductsInTheInventory()
        {
            try
            {
                var result = await mediator.Send(new GetAllProductsInTheInventoryQuery(new GetAllProductsInTheInventoryDTO()));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseDTO<List<GetAllProductsInTheInventoryDTO>>.Error(ErrorCode.UnExcepectedError, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpGet("products below their LowStockThreshold")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetProductsBelowLowStockThreshold()
        {
            try
            {
                var result = await mediator.Send(new GetProductsBelowLowStockThresholdQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ResponseDTO<List<ProductsBelowLowStockThresholdDTO>>.Error(ErrorCode.UnExcepectedError, $"Internal server error: {ex.Message}"));
            }
        }
    }
}
