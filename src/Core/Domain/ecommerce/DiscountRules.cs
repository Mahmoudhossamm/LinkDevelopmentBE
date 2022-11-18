using FSH.WebApi.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.ecommerce;

public class DiscountRules : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public int Quantity { get; set; }
    public decimal Percentage { get; private set; }
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = default!;


    public DiscountRules(string name, string description, int quantity, decimal percentage, Guid productId)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        Percentage = percentage;
        ProductId = productId;
    }

    public DiscountRules Update(string? name, string? description, int? quantity, decimal? percentage, Guid? productId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (percentage.HasValue && Percentage != percentage) Percentage = percentage.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        if (productId.HasValue && productId.Value != Guid.Empty && !productId.Equals(productId.Value)) productId = productId.Value;
        return this;
    }
}
