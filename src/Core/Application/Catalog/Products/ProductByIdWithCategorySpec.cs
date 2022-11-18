namespace FSH.WebApi.Application.Catalog.Products;

public class ProductByIdWithCategorySpec : Specification<Product, ProductDetailsDto>, ISingleResultSpecification
{
    public ProductByIdWithCategorySpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Category);
}