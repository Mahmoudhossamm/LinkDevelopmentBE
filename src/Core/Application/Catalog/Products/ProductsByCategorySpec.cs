namespace FSH.WebApi.Application.Catalog.Products;

public class ProductsByCategorySpec : Specification<Product>
{
    public ProductsByCategorySpec(Guid categoryId) =>
        Query.Where(p => p.CategoryId == categoryId);
}
