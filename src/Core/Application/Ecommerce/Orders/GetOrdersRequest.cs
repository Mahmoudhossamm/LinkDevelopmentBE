using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class GetOrdersRequest : IRequest<OrdersDto>
{
    public Guid Id { get; set; }

    public GetOrdersRequest(Guid id) => Id = id;
}

public class OrdersByIdSpec : Specification<Orders, OrdersDto>, ISingleResultSpecification
{
    public OrdersByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetOrdersRequestHandler : IRequestHandler<GetOrdersRequest, OrdersDto>
{
    private readonly IRepository<Orders> _repository;
    private readonly IStringLocalizer _t;

    public GetOrdersRequestHandler(IRepository<Orders> repository, IStringLocalizer<GetOrdersRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<OrdersDto> Handle(GetOrdersRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Orders, OrdersDto>)new OrdersByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Orders {0} Not Found.", request.Id]);
}