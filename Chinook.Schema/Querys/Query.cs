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
        //public async Task<Customer> SingleCustomer([Service] ChinookContext context, int id)
        //{
        //    var result = await context.Customer.FirstOrDefaultAsync(customer => customer.CustomerId == id);

        //    return result;
        //}
        public async Task<IQueryable<Customer>> Customers([Service] ChinookContext context)
        {
            var result = await context.Customer.ToListAsync();

            return result.AsQueryable();
        }


        public async Task<IQueryable<Artist>> Artists([Service] ChinookContext context)
        {
            var result = await context.Artist.ToListAsync();

            return result.AsQueryable();
        }

        public async Task<IQueryable<Invoice>> Invoices([Service] ChinookContext context)
        {
            var result = await context.Invoice.ToListAsync();

            return result.AsQueryable();
        }
    }
}
