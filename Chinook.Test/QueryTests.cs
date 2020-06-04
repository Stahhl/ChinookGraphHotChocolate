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
using Newtonsoft.Json;
using Chinook.Schema.Models;
using Newtonsoft.Json.Linq;
using System.Linq;

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
        /// <summary>
        /// 4. Provide a query showing only the Employees who are Sales Agents.
        /// </summary>
        [Fact]
        public async void Test_04()
        {
            string gql =
                "query{" +
                  "employees(where: {title_contains: \"Agent\"}){" +
                        "firstName " +
                        "lastName " +
                        "title " +
                    "}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var result = await executor.ExecuteAsync(request);

            Assert.Equal(0, result.Errors.Count);
        }
        /// <summary>
        /// 5. Provide a query showing a unique list of billing countries from the Invoice table.
        /// No disctinct filter for hot chocolate yet might aswell practice linq / JOBject
        /// </summary>
        [Fact]
        public async void Test_05()
        {
            string gql =
                "query{" +
                  "invoices{" +
                        "billingCountry " +
                    "}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var result = await executor.ExecuteAsync(request);

            var json = JsonConvert.SerializeObject(result);
            var jObj = JObject.Parse(json);

            var countries =
                from p in jObj["Data"]["invoices"]
                select (string)p["billingCountry"];

            var distinct = countries.Select(x => x).Distinct();

            Assert.Equal(0, result.Errors.Count);
            Assert.True(countries.Count() > distinct.Count());
        }
        /// <summary>
        /// 6. Provide a query that shows the invoices associated with each sales agent. The resultant table should include the Sales Agent's full name.
        /// </summary>
        [Fact]
        public async void Test_06()
        {
            string gql =
                "query{" +
                  "employees(where: {title_contains: \"Agent\"}){" +
                        "firstName " +
                        "lastName " +
                        "customers{ " +
                            "invoices{ " +
                                "invoiceId" +
                    "}}}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var result = await executor.ExecuteAsync(request);

            Assert.Equal(0, result.Errors.Count);
        }
        /// <summary>
        /// 7. Provide a query that shows the Invoice Total, Customer name, Country and Sale Agent name for all invoices and customers.
        /// </summary>
        [Fact]
        public async void Test_07()
        {
            string gql =
                "query{" +
                  "customers{" +
                        "firstName " +
                        "lastName " +
                        "country " +
                        "supportRep{ " +
                            "firstName " +
                            "lastName " +
                         "}" +
                         "invoices{ " +
                            "total" +
                    "}}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var result = await executor.ExecuteAsync(request);

            Assert.Equal(0, result.Errors.Count);
        }
    }
}
