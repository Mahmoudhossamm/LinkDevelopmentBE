namespace FSH.WebApi.Domain.Catalog;

public class Product : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public Guid CategoryId { get; private set; }
    public virtual Category Category { get; private set; } = default!;

    public Product(string name, string? description, int quantity, decimal price, Guid categoryId)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        Price = price;
        CategoryId = categoryId;
    }

    public Product Update(string? name, string? description, int? quantity, decimal? price, Guid? categoryId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (price.HasValue && Price != price) Price = price.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        if (categoryId.HasValue && categoryId.Value != Guid.Empty && !categoryId.Equals(categoryId.Value)) CategoryId = categoryId.Value;
        return this;
    }

  
}