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
        public IQueryable<Customer> Customers([Service] ChinookContext context)
        {
            var result = context.Customer;

            return result;
        }


        public IQueryable<Artist> Artists([Service] ChinookContext context)
        {
            var result = context.Artist;

            return result;
        }

        public IQueryable<Invoice> Invoices([Service] ChinookContext context)
        {
            var result = context.Invoice;

            return result;
        }

        public IQueryable<Employee> Employees([Service] ChinookContext context)
        {
            var result = context.Employee;

            return result;
        }
    }
}
