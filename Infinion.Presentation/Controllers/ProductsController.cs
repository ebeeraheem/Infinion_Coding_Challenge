using Infinion.Application.HelperMethods;
using Infinion.Application.Services.Interfaces;
using Infinion.Domain.DTOs;
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

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <param name="category">The category to filter by.</param>
    /// <param name="minPrice">The minimum price to filter by.</param>
    /// <param name="maxPrice">The maximum price to filter by.</param>
    /// <param name="inStock">Whether to filter by stock availability.</param>
    /// <param name="name">A partial match of the product name to filter by.</param>
    /// <param name="startDate">The start date for the created or updated date range filter.</param>
    /// <param name="endDate">The end date for the created or updated date range filter.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="pageSize">The number of items per page for pagination.</param>
    /// <returns>A list of products based on the provided filters.</returns>
    /// <response code="200">Returns the list of products.</response>
    /// <response code="401">If the user is unauthorised.</response>
    /// <response code="404">If no product matches the specified filters.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(
        [FromQuery] string? category = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] bool? inStock = null,
        [FromQuery] string? name = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();

            var pagedResult = products.FilterProducts(
                category,
                minPrice,
                maxPrice,
                inStock,
                name,
                startDate,
                endDate,
                page,
                pageSize);

            Response.Headers.Append("X-Total-Count", pagedResult.TotalCount.ToString());

            if (!pagedResult.Items.Any())
            {
                return NotFound("No products match the specified filters. Please adjust your search criteria.");
            }

            return Ok(pagedResult.Items);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured.");
        }        
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The product with the specified ID.</returns>
    /// <response code="200">Returns the product with the specified ID.</response>
    /// <response code="401">If the user is unauthorised.</response>
    /// <response code="404">If the product with the specified ID is not found.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Product>> GetProductById(int id)
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

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="productCreationDto">The product creation DTO containing the details of the new product.</param>
    /// <returns>The created product.</returns>
    /// <response code="201">Returns the created product.</response>
    /// <response code="400">If the product creation DTO is invalid.</response>
    /// <response code="401">If the user is unauthorised.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductCreationDto productCreationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var newProduct = await _productService.CreateProductAsync(productCreationDto);

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
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

    /// <summary>
    /// Updates an existing product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="product">The updated product details.</param>
    /// <returns>The updated product.</returns>
    /// <response code="200">Returns the updated product.</response>
    /// <response code="400">If the product update details are invalid.</response>
    /// <response code="401">If the user is unauthorised.</response>
    /// <response code="404">If the product with the specified ID is not found.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
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

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A response indicating the result of the delete operation.</returns>
    /// <response code="204">If the product was successfully deleted.</response>
    /// <response code="401">If the user is unauthorised.</response>
    /// <response code="404">If the product with the specified ID is not found.</response>
    /// <response code="500">If there was an internal server error.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        try
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting product with id {id}: {ex.Message}");
        }
    }
}
