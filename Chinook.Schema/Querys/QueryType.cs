using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.Schema.Querys
{
    public class QueryType : ObjectType<Query>
    {
        //protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        //{
        //    descriptor.Field(t => t.GetPersons(default))
        //        .Type<ListType<NonNullType<PersonType>>>()
        //        .UseFiltering();
        //}
    }
}
