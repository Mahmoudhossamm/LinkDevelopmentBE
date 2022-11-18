namespace FSH.WebApi.Application.Catalog.Categories;

public class CategorydByNameSpec : Specification<Category>, ISingleResultSpecification
{
    public CategorydByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}