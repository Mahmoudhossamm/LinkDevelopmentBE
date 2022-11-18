namespace FSH.WebApi.Application.Catalog.Products;

public class ProductsBySearchRequestWithCategorysSpec : EntitiesByPaginationFilterSpec<Product, ProductDto>
{
    public ProductsBySearchRequestWithCategorysSpec(SearchProductsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Category)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.CategoryId.Equals(request.CategoryId!.Value), request.CategoryId.HasValue);
           
}