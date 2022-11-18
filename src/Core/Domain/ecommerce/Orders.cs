using FSH.WebApi.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.ecommerce;

public class Orders : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = default!;

    public Orders(string name, string? description, decimal price, decimal discountPercentage, int quantity, Guid productId)
    {
        Name = name;
        Description = description;
        Price = price;
        DiscountPercentage = discountPercentage;
        Quantity = quantity;
        ProductId = productId;
    }

    public Orders Update(string? name, string? description, decimal? price, decimal? discountPercentage, int? quantity, Guid? productId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (price.HasValue && Price != price) Price = price.Value;
        if (discountPercentage.HasValue && DiscountPercentage != discountPercentage) DiscountPercentage = discountPercentage.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        if (productId.HasValue && productId.Value != Guid.Empty && !productId.Equals(productId.Value)) productId = productId.Value;
        return this;
    }
}
