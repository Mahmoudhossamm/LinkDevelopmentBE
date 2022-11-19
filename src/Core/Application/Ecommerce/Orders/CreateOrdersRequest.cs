using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class CreateOrdersRequest : IRequest<Guid>
{
    public string Name { get;  set; } = default!;
    public string? Description { get; set; }

    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal DiscountPercentage { get; set; }
    public Guid ProductId { get; set; }
}

public class CreateOrdersRequestValidator : CustomValidator<CreateOrdersRequest>
{
    public CreateOrdersRequestValidator(IReadRepository<Orders> repository, IStringLocalizer<CreateOrdersRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new OrdersByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Orders {0} already Exists.", name]);
}

public class CreateOrdersRequestHandler : IRequestHandler<CreateOrdersRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Orders> _repository;

    public CreateOrdersRequestHandler(IRepositoryWithEvents<Orders> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateOrdersRequest request, CancellationToken cancellationToken)
    {
        var Orders = new Orders(request.Name, request.Description, request.Price, request.DiscountPercentage,request.Quantity, request.ProductId);

        await _repository.AddAsync(Orders, cancellationToken);

        return Orders.Id;
    }
}