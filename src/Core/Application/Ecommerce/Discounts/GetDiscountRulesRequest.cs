using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class GetDiscountRulesRequest : IRequest<DiscountRulesDto>
{
    public Guid Id { get; set; }

    public GetDiscountRulesRequest(Guid id) => Id = id;
}

public class DiscountRulesByIdSpec : Specification<DiscountRules, DiscountRulesDto>, ISingleResultSpecification
{
    public DiscountRulesByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetDiscountRulesRequestHandler : IRequestHandler<GetDiscountRulesRequest, DiscountRulesDto>
{
    private readonly IRepository<DiscountRules> _repository;
    private readonly IStringLocalizer _t;

    public GetDiscountRulesRequestHandler(IRepository<DiscountRules> repository, IStringLocalizer<GetDiscountRulesRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<DiscountRulesDto> Handle(GetDiscountRulesRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<DiscountRules, DiscountRulesDto>)new DiscountRulesByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["DiscountRules {0} Not Found.", request.Id]);
}