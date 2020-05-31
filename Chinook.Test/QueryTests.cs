using Chinook.Domain;
using Chinook.Domain.Models;
using Chinook.Schema;
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
        /// 1. Provide a query showing Customers (just their full names, customer ID and country) who are not in the US.
        /// </summary>
        [Fact]
        public async void Test_01()
        {
            string gql =
                "query{" +
                  "customers(where: {country_not_contains: \"USA\"}){ " +
                        "firstName " +
                        "lastName " +
                        "customerId " +
                        "country " +
                    "}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var result = await executor.ExecuteAsync(request);

            Assert.Equal(0, result.Errors.Count);
        }
        /// <summary>
        /// 2. Provide a query only showing the Customers from Brazil.
        /// </summary>
        [Fact]
        public async void Test_02()
        {
            string gql =
                "query{" +
                  "customers(where: { country_contains: \"Brazil\"}){ " +
                        "firstName " + 
                        "lastName " + 
                    "}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var result = await executor.ExecuteAsync(request);

            Assert.Equal(0, result.Errors.Count);
        }
        /// <summary>
        /// 3. Provide a query showing the Invoices of customers who are from Brazil. 
        /// The resultant table should show the customer's full name, Invoice ID, Date of the invoice and billing country.
        /// </summary>
        [Fact]
        public async void Test_03()
        {
            string gql =
                "query{" +
                  "customers(where: { country_contains: \"Brazil\"}){ " +
                        "firstName " +
                        "lastName " +
                        "customerId " +
                        "country " +
                        "invoice{ " +
                            "invoiceId " +
                            "invoiceDate " +
                            "billingCountry " +
                    "}}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var result = await executor.ExecuteAsync(request);

            Assert.Equal(0, result.Errors.Count);
        }
    }
}
