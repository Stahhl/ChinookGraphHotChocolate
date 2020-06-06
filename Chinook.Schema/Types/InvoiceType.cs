using Chinook.Domain;
using Chinook.Domain.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Chinook.Common.StringHelper;

namespace Chinook.Schema.Types
{
    class InvoiceType : ObjectType<Invoice>
    {
        protected override void Configure(IObjectTypeDescriptor<Invoice> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field<InvoiceType>(r => r.Customer(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(InvoiceType.Customer)));

            descriptor.Field<InvoiceType>(r => r.InvoiceLines(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(InvoiceType.InvoiceLines)));
        }

        //customer, invoiceline
        public Customer Customer([Service] ChinookContext context, [Parent] Invoice invoice)
        {
            var result = context.Customer.FirstOrDefault(i => i.CustomerId == invoice.CustomerId);

            return result;
        }
        public IQueryable<InvoiceLine> InvoiceLines([Service] ChinookContext context, [Parent] Invoice invoice)
        {
            var result = context.InvoiceLine.Where(t => t.InvoiceId == invoice.InvoiceId);

            return result;
        }
    }


}
