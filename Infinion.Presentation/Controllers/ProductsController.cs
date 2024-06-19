using Infinion.Application.Services.Interfaces;
using Infinion.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infinion.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
        }        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var newProduct = await _productService.CreateProductAsync(product);

            return CreatedAtAction(nameof(GetProductByIdAsync), new { id = newProduct.Id }, newProduct);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest($"Invalid product: {ex.Message}.");
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating product: {ex.Message}");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProductAsync(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest("Id mismatch");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            return Ok(updatedProduct);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest($"Invalid product: {ex.Message}.");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating product with id {id}: {ex.Message}");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProductAsync(int id)
    {
        try
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(true);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting product with id {id}: {ex.Message}");
        }
    }
}
