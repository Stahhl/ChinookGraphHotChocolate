﻿using Chinook.Schema.Filters;
using Chinook.Schema.Types;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.Schema.Querys
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(t => t.Customers(default))
                .Type<ListType<NonNullType<CustomerType>>>()
                .UseFiltering<DefaultFilterType>();

            descriptor.Field(t => t.Artists(default))
                .Type<ListType<NonNullType<ArtistType>>>()
                .UseFiltering<DefaultFilterType>();

            descriptor.Field(t => t.Invoices(default))
                .Type<ListType<NonNullType<InvoiceType>>>()
                .UseFiltering<DefaultFilterType>();
        }
    }
}
