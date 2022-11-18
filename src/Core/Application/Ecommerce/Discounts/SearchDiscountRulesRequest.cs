using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;


public class SearchDiscountRulesRequest : PaginationFilter, IRequest<PaginationResponse<DiscountRulesDto>>
{
}

public class DiscountRulesBySearchRequestSpec : EntitiesByPaginationFilterSpec<DiscountRules, DiscountRulesDto>
{
    public DiscountRulesBySearchRequestSpec(SearchDiscountRulesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchDiscountRulesRequestHandler : IRequestHandler<SearchDiscountRulesRequest, PaginationResponse<DiscountRulesDto>>
{
    private readonly IReadRepository<DiscountRules> _repository;

    public SearchDiscountRulesRequestHandler(IReadRepository<DiscountRules> repository) => _repository = repository;

    public async Task<PaginationResponse<DiscountRulesDto>> Handle(SearchDiscountRulesRequest request, CancellationToken cancellationToken)
    {
        var spec = new DiscountRulesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
