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

            descriptor.Field<CustomerType>(r => r.GetInvoices(default, default))
                .UseFiltering()
                .Name(nameof(Customer.Invoice).ToLower());
        }

        public IEnumerable<Invoice> GetInvoices([Service] ChinookContext context, [Parent] Customer customer)
        {
            return context.Invoice.Where(i => i.CustomerId == customer.CustomerId);
        }
    }
}
