using FSH.WebApi.Application.Ecommerce.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers.Ecommerce;
public class DiscountRulesController : VersionedApiController
{
    [HttpPost("search")]
    [OpenApiOperation("Search Discount Rules using available filters.", "")]
    public Task<PaginationResponse<DiscountRulesDto>> SearchAsync(SearchDiscountRulesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get product details.", "")]
    public Task<DiscountRulesDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetDiscountRulesRequest(id));
    }

    [HttpPost]
    [OpenApiOperation("Create a new Discount Rule.", "")]
    public Task<Guid> CreateAsync(CreateDiscountRulesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a DiscountRules.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateDiscountRulesRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a DiscountRules.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteDiscountRulesRequest(id));
    }
}
