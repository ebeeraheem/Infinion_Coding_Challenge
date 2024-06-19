using System.ComponentModel.DataAnnotations;

namespace Infinion.Domain.DTOs;
public class ProductCreationDto
{
    [Required]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(1, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, 10000, ErrorMessage = "Stock must be between 0 and 10,000.")]
    public int Stock { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Category must be between 2 and 50 characters.")]
    public string Category { get; set; } = string.Empty;

    [Required]
    public Uri? ImageUrl { get; set; }
}
