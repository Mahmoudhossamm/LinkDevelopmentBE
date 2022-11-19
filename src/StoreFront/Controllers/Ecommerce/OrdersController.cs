using FSH.WebApi.Application.Ecommerce.Discounts;
using Microsoft.AspNetCore.Mvc;

namespace FSH.WebApi.Host.Controllers.Ecommerce;
public class OrdersController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [OpenApiOperation("Search Discount Rules using available filters.", "")]
    public Task<PaginationResponse<OrdersDto>> SearchAsync(SearchOrdersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [OpenApiOperation("Get product details.", "")]
    public Task<OrdersDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrdersRequest(id));
    }

    [HttpPost]
    [AllowAnonymous]
    [OpenApiOperation("Create a new Discount Rule.", "")]
    public Task<Guid> CreateAsync(CreateOrdersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a Orders.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateOrdersRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a Orders.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteOrdersRequest(id));
    }
}
