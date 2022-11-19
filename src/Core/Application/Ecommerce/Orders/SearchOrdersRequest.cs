using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;


public class SearchOrdersRequest : PaginationFilter, IRequest<PaginationResponse<OrdersDto>>
{
}

public class OrdersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Orders, OrdersDto>
{
    public OrdersBySearchRequestSpec(SearchOrdersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchOrdersRequestHandler : IRequestHandler<SearchOrdersRequest, PaginationResponse<OrdersDto>>
{
    private readonly IReadRepository<Orders> _repository;

    public SearchOrdersRequestHandler(IReadRepository<Orders> repository) => _repository = repository;

    public async Task<PaginationResponse<OrdersDto>> Handle(SearchOrdersRequest request, CancellationToken cancellationToken)
    {
        var spec = new OrdersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
