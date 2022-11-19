using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class DeleteOrdersRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteOrdersRequest(Guid id) => Id = id;
}

public class DeleteOrdersRequestHandler : IRequestHandler<DeleteOrdersRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Orders> _OrdersRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    public DeleteOrdersRequestHandler(IRepositoryWithEvents<Orders> OrdersRepo, IReadRepository<Product> productRepo, IStringLocalizer<DeleteOrdersRequestHandler> localizer) =>
        (_OrdersRepo, _productRepo, _t) = (OrdersRepo, productRepo, localizer);

    public async Task<Guid> Handle(DeleteOrdersRequest request, CancellationToken cancellationToken)
    {
        //if (await _productRepo.AnyAsync(new ProductsByOrdersSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_t["Orders cannot be deleted as it's being used."]);
        //}

        var Orders = await _OrdersRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = Orders ?? throw new NotFoundException(_t["Orders {0} Not Found."]);

        await _OrdersRepo.DeleteAsync(Orders, cancellationToken);

        return request.Id;
    }
}