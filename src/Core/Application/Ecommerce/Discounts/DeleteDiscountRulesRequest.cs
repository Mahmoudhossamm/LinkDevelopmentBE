using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class DeleteDiscountRulesRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteDiscountRulesRequest(Guid id) => Id = id;
}

public class DeleteDiscountRulesRequestHandler : IRequestHandler<DeleteDiscountRulesRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<DiscountRules> _DiscountRulesRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    public DeleteDiscountRulesRequestHandler(IRepositoryWithEvents<DiscountRules> DiscountRulesRepo, IReadRepository<Product> productRepo, IStringLocalizer<DeleteDiscountRulesRequestHandler> localizer) =>
        (_DiscountRulesRepo, _productRepo, _t) = (DiscountRulesRepo, productRepo, localizer);

    public async Task<Guid> Handle(DeleteDiscountRulesRequest request, CancellationToken cancellationToken)
    {
        //if (await _productRepo.AnyAsync(new ProductsByDiscountRulesSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_t["DiscountRules cannot be deleted as it's being used."]);
        //}

        var DiscountRules = await _DiscountRulesRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = DiscountRules ?? throw new NotFoundException(_t["DiscountRules {0} Not Found."]);

        await _DiscountRulesRepo.DeleteAsync(DiscountRules, cancellationToken);

        return request.Id;
    }
}