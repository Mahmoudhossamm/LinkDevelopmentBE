using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Products;

public class CreateProductRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity  { get; set; }
    public Guid CategoryId { get; set; }
}

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Guid>
{
    private readonly IRepository<Product> _repository;

    public CreateProductRequestHandler(IRepository<Product> repository) =>
        (_repository) = (repository);

    public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
       var product = new Product(request.Name, request.Description, request.Quantity, request.Price,request.CategoryId);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityCreatedEvent.WithEntity(product));

        await _repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}