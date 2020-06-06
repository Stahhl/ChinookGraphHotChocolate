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
    public class InvoiceLineType : ObjectType<InvoiceLine>
    {
        protected override void Configure(IObjectTypeDescriptor<InvoiceLine> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field<InvoiceLineType>(r => r.Invoice(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(InvoiceLineType.Invoice)));

            descriptor.Field<InvoiceLineType>(r => r.Track(default, default))
                .UseFiltering()
                .Name(camelCase(nameof(InvoiceLineType.Track)));
        }

        //invoice, track
        public Invoice Invoice([Service] ChinookContext context, [Parent] InvoiceLine invoiceLine)
        {
            var result = context.Invoice.FirstOrDefault(i => i.InvoiceId == invoiceLine.InvoiceId);

            return result;
        }
        public Track Track([Service] ChinookContext context, [Parent] InvoiceLine invoiceLine)
        {
            var result = context.Track.FirstOrDefault(t => t.TrackId == invoiceLine.TrackId);

            return result;
        }
    }
}
