using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class CreateDiscountRulesRequest : IRequest<Guid>
{
    public string Name { get;  set; } = default!;
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal Percentage { get; set; }
    public Guid? ProductId { get; set; }
}

public class CreateDiscountRulesRequestValidator : CustomValidator<CreateDiscountRulesRequest>
{
    public CreateDiscountRulesRequestValidator(IReadRepository<DiscountRules> repository, IStringLocalizer<CreateDiscountRulesRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new DiscountRulesdByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["DiscountRules {0} already Exists.", name]);
}

public class CreateDiscountRulesRequestHandler : IRequestHandler<CreateDiscountRulesRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<DiscountRules> _repository;

    public CreateDiscountRulesRequestHandler(IRepositoryWithEvents<DiscountRules> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateDiscountRulesRequest request, CancellationToken cancellationToken)
    {
        var DiscountRules = new DiscountRules(request.Name, request.Description, request.Quantity,request.Percentage, request.ProductId);

        await _repository.AddAsync(DiscountRules, cancellationToken);

        return DiscountRules.Id;
    }
}