using Chinook.Domain;
using Chinook.Domain.Models;
using Chinook.Schema.Querys;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace Chinook.Test
{
    //https://chillicream.com/blog/2019/04/11/integration-tests#schema-tests
    public class QueryTests
    {
        /// <summary>
        /// 2. Provide a query only showing the Customers from Brazil.
        /// </summary>
        [Fact]
        public async void Test_02()
        {
            string gql =
                "query{" +
                  "allCustomers(where: { country_contains: \"Brazil\"}){ " +
                        "firstName " + 
                        "lastName " + 
                    "}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var result = await executor.ExecuteAsync(request);

            Assert.Equal(0, result.Errors.Count);
        }
    }
}
