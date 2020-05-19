using Chinook.Domain;
using Chinook.Domain.Models;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Schema.Querys
{
    public class Query
    {
        public string Hello => "world";

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<Customer>> GetCustomers([Service] ChinookContext context)
        {
            var result = await context.Customer.ToListAsync();

            return result.AsQueryable();
        }
    }
}
