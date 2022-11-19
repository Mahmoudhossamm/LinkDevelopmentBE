using FSH.WebApi.Domain.ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Ecommerce.Discounts;

public class OrdersByNameSpec : Specification<Orders>, ISingleResultSpecification
{
    public OrdersByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}