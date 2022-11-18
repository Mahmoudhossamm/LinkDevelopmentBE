namespace FSH.WebApi.Application.Catalog.Products;

public class CreateProductRequestValidator : CustomValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(IReadRepository<Product> productRepo, IReadRepository<Category> brandRepo, IStringLocalizer<CreateProductRequestValidator> T)
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await productRepo.GetBySpecAsync(new ProductByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Product {0} already Exists.", name]);

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(1);


        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await brandRepo.GetByIdAsync(id, ct) is not null)
                .WithMessage((_, id) => T["Brand {0} Not Found.", id]);
    }
}