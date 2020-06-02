using Chinook.Domain;
using Chinook.Domain.Models;
using Chinook.Schema.Resolvers;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Schema.Types
{
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field<CustomerType>(r => r.SupportRep(default, default))
                .UseFiltering()
                .Name(nameof(CustomerType.SupportRep).ToLower());

            descriptor.Field<CustomerType>(r => r.Invoices(default, default))
                .UseFiltering()
                .Name(nameof(CustomerType.Invoices).ToLower());
        }

        public Employee SupportRep([Service] ChinookContext context, [Parent] Customer customer)
        {
            var result = context.Employee.FirstOrDefault(e => e.EmployeeId == customer.SupportRepId);

            return result;
        }
        public IQueryable<Invoice> Invoices([Service] ChinookContext context, [Parent] Customer customer)
        {
            var result = context.Invoice.Where(i => i.CustomerId == customer.CustomerId);

            return result;
        }
    }
}
