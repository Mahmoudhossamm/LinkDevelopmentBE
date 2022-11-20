using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;


public class SearchDiscountRulesByProductRequest : PaginationFilter, IRequest<PaginationResponse<DiscountRulesDto>>
{
    public Guid? ProductId { get; set; }
}

public class DiscountRulesBySearchProductRequestSpec : EntitiesByPaginationFilterSpec<DiscountRules, DiscountRulesDto>
{
    public DiscountRulesBySearchProductRequestSpec(SearchDiscountRulesByProductRequest request)
        : base(request) =>
        Query
        .Where(x => x.ProductId == request.ProductId)
        .OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchDiscountRulesByProductRequestHandler : IRequestHandler<SearchDiscountRulesByProductRequest, PaginationResponse<DiscountRulesDto>>
{
    private readonly IReadRepository<DiscountRules> _repository;

    public SearchDiscountRulesByProductRequestHandler(IReadRepository<DiscountRules> repository) => _repository = repository;

    public async Task<PaginationResponse<DiscountRulesDto>> Handle(SearchDiscountRulesByProductRequest request, CancellationToken cancellationToken)
    {
        var spec = new DiscountRulesBySearchProductRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
