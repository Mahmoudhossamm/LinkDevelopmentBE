namespace FSH.WebApi.Application.Catalog.Products;

public class ProductDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? Quantity { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
}