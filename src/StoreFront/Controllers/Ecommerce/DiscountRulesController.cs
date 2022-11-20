using FSH.WebApi.Application.Ecommerce.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers.Ecommerce;
public class DiscountRulesController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    [OpenApiOperation("Search Discount Rules using available filters.", "")]
    public Task<PaginationResponse<DiscountRulesDto>> SearchAsync(SearchDiscountRulesByProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]

    [OpenApiOperation("Get product details.", "")]
    public Task<DiscountRulesDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetDiscountRulesRequest(id));
    }

    

  
}
