using FSH.WebApi.Application.Catalog.Categories;

namespace FSH.WebApi.Application.Catalog.Products;

public class ProductDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? Quantity { get; set; }
    public CategoryDto Category { get; set; } = default!;
}