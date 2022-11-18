using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class DiscountRulesdByNameSpec : Specification<DiscountRules>, ISingleResultSpecification
{
    public DiscountRulesdByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}