using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class DiscountRulesDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get;  set; } = default!;
    public string? Description { get;  set; }
    public int Quantity { get; set; }
    public decimal Percentage { get; set; }

    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
}
