using HotChocolate;
using System;
using System.Collections.Generic;

namespace Chinook.Domain.Models
{
    public partial class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        [GraphQLIgnore]
        public virtual Invoice Invoice { get; set; }
        [GraphQLIgnore]
        public virtual Track Track { get; set; }
    }
}
