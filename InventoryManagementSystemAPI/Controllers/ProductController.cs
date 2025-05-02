using InventoryManagementSystemAPI.CQRS.Commands.ProductCommands;
using InventoryManagementSystemAPI.CQRS.Queries.ProductQueries;
using InventoryManagementSystemAPI.DTO.Product;
using InventoryManagementSystemAPI.DTO.ProductDTO;
using InventoryManagementSystemAPI.Models;
using MediatR;
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
        public async Task<IActionResult> AddProduct(AddProductCommand command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await mediator.Send(command);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO productDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UpdateProductCommand command = new UpdateProductCommand(id, productDTO);
                    UpdateProductDTO result = await mediator.Send(command);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                RemoveProductCommand command = new RemoveProductCommand(id);
                await mediator.Send(command);
                if (command == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                GetAllProductQuery command = new GetAllProductQuery(new AllProductsDTO());
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                GetProductByIdQuery command = new GetProductByIdQuery(id, new GetProductByIdDTO());
                var result = await mediator.Send(command);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetProductsInTheInventory")]
        public async Task<IActionResult> GetProductsInTheInventory()
        {
            try
            {
                GetAllProductsInTheInventoryQuery command = new GetAllProductsInTheInventoryQuery(new GetAllProductsInTheInventoryDTO());
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
