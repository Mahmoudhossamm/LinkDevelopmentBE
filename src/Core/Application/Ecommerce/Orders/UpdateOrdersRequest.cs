using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class UpdateOrdersRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get;  set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal DiscountPercentage { get; set; }
    public Guid ProductId { get; set; }
}

public class UpdateOrdersRequestValidator : CustomValidator<UpdateOrdersRequest>
{
    public UpdateOrdersRequestValidator(IRepository<Orders> repository, IStringLocalizer<UpdateOrdersRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (Orders, name, ct) =>
                    await repository.GetBySpecAsync(new OrdersByNameSpec(name), ct)
                        is not Orders existingOrders || existingOrders.Id == Orders.Id)
                .WithMessage((_, name) => T["Orders {0} already Exists.", name]);
}

public class UpdateOrdersRequestHandler : IRequestHandler<UpdateOrdersRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Orders> _repository;
    private readonly IStringLocalizer _t;

    public UpdateOrdersRequestHandler(IRepositoryWithEvents<Orders> repository, IStringLocalizer<UpdateOrdersRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateOrdersRequest request, CancellationToken cancellationToken)
    {
        var Orders = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = Orders
        ?? throw new NotFoundException(_t["Orders {0} Not Found.", request.Id]);

        Orders.Update(request.Name, request.Description,request.Price, request.DiscountPercentage,request.Quantity,request.ProductId);

        await _repository.UpdateAsync(Orders, cancellationToken);

        return request.Id;
    }
}