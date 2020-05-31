using Chinook.Domain;
using Chinook.Schema.Querys;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.Schema
{
    public static class ComponentFactory
    {
        public static IServiceProvider GetServiceProvider()
        {
            var result = new ServiceCollection().AddTransient<ChinookContext>().BuildServiceProvider();

            return result;
        }
        public static SchemaBuilder GetSchemaBuilder()
        {
            var result = new SchemaBuilder();
            result.AddQueryType<QueryType>();

            return result;
        }
        public static ISchema GetSchema(SchemaBuilder schemaBuilder)
        {
            var result = schemaBuilder.Create();

            return result;
        }
        public static IQueryExecutor GetQueryExecutor()
        {
            var schemaBuilder = GetSchemaBuilder();
            var schema = GetSchema(schemaBuilder);
            var result = schema.MakeExecutable();

            return result;
        }
        public static IReadOnlyQueryRequest GetQueryRequest(IServiceProvider serviceProvider, string gql)
        {
            var result = QueryRequestBuilder.New()
                .SetQuery(gql)
                .SetServices(serviceProvider)
                .AddProperty("Key", "value")
                .Create();

            return result;
        }
    }
}
