using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class UpdateDiscountRulesRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get;  set; }
    public int Quantity { get; set; }
    public decimal Percentage { get; set; }
    public Guid ProductId { get; set; }
}

public class UpdateDiscountRulesRequestValidator : CustomValidator<UpdateDiscountRulesRequest>
{
    public UpdateDiscountRulesRequestValidator(IRepository<DiscountRules> repository, IStringLocalizer<UpdateDiscountRulesRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (DiscountRules, name, ct) =>
                    await repository.GetBySpecAsync(new DiscountRulesdByNameSpec(name), ct)
                        is not DiscountRules existingDiscountRules || existingDiscountRules.Id == DiscountRules.Id)
                .WithMessage((_, name) => T["DiscountRules {0} already Exists.", name]);
}

public class UpdateDiscountRulesRequestHandler : IRequestHandler<UpdateDiscountRulesRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<DiscountRules> _repository;
    private readonly IStringLocalizer _t;

    public UpdateDiscountRulesRequestHandler(IRepositoryWithEvents<DiscountRules> repository, IStringLocalizer<UpdateDiscountRulesRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateDiscountRulesRequest request, CancellationToken cancellationToken)
    {
        var DiscountRules = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = DiscountRules
        ?? throw new NotFoundException(_t["DiscountRules {0} Not Found.", request.Id]);

        DiscountRules.Update(request.Name, request.Description, request.Quantity, request.Percentage,request.ProductId);

        await _repository.UpdateAsync(DiscountRules, cancellationToken);

        return request.Id;
    }
}