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
using System.Globalization;

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

            var response = await executor.ExecuteAsync(request);

            Assert.Equal(0, response.Errors.Count);
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

            var response = await executor.ExecuteAsync(request);

            Assert.Equal(0, response.Errors.Count);
        }
        /// <summary>
        /// 3. Provide a query showing the Invoices of customers who are from Brazil. 
        /// The responseant table should show the customer's full name, Invoice ID, Date of the invoice and billing country.
        /// </summary>
        [Fact]
        public async void Test_03()
        {
            string gql =
                "query{" +
                  "customers(where: { country: \"Brazil\"}){ " +
                        "firstName " +
                        "lastName " +
                        "customerId " +
                        "country " +
                        "invoices{ " +
                            "invoiceId " +
                            "invoiceDate " +
                            "billingCountry " +
                    "}}}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var response = await executor.ExecuteAsync(request);

            Assert.Equal(0, response.Errors.Count);
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

            var response = await executor.ExecuteAsync(request);

            Assert.Equal(0, response.Errors.Count);
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

            var response = await executor.ExecuteAsync(request);

            var json = JsonConvert.SerializeObject(response);
            var jObj = JObject.Parse(json);

            var countries =
                from p in jObj["Data"]["invoices"]
                select (string)p["billingCountry"];

            var distinct = countries.Select(x => x).Distinct();

            Assert.Equal(0, response.Errors.Count);
            Assert.True(countries.Count() > distinct.Count());
        }
        /// <summary>
        /// 6. Provide a query that shows the invoices associated with each sales agent. The responseant table should include the Sales Agent's full name.
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

            var response = await executor.ExecuteAsync(request);

            Assert.Equal(0, response.Errors.Count);
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

            var response = await executor.ExecuteAsync(request);

            Assert.Equal(0, response.Errors.Count);
        }
        /// <summary>
        /// 8. How many Invoices were there in 2009 and 2011? What are the respective total sales for each of those years?
        /// </summary>
        [Fact]
        public async void Test_08()
        {
            string gql =
                "query{" +
                    "year2009: invoices(where: {AND: [{invoiceDate_gt: \"2009-01-01\"}, {invoiceDate_lt: \"2009-12-31\"}]}){ " +
                        "total " +
                    "}" +
                     "year2011: invoices(where: {AND: [{invoiceDate_gt: \"2011-01-01\"}, {invoiceDate_lt: \"2011-12-31\"}]}){ " +
                        "total " +
                    "}" +
                 "}";

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var response = await executor.ExecuteAsync(request);

            var json = JsonConvert.SerializeObject(response);
            var jObj = JObject.Parse(json);

            var year2009 =
                from p in jObj["Data"]["year2009"]
                select (string)p["total"];

            var year2011 =
                from p in jObj["Data"]["year2011"]
                select (string)p["total"];

            var period = year2009.Concat(year2011);

            var numberOfInvoices = period.Count();
            var totalSales = period.Select(x => decimal.Parse(x, new CultureInfo("en-GB"))).Sum();

            Assert.Equal(0, response.Errors.Count);
            Assert.Equal(166, numberOfInvoices);
            Assert.Equal((decimal)919.04, totalSales);
        }

        /// <summary>
        /// 9. Loking at the InvoiceLine table, provide a query that COUNTs the number of line items for Invoice ID 37.
        /// </summary>
        [Fact]
        public async void Test_09()
        {
            string gql =
                "query ($id: Int!){" +
                    "invoice(id: $id){ " +
                        "invoiceLines{ " +
                            "quantity" +
                "}}}";

            var variables = new Dictionary<string, object>() { { "id", 37 } };

            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql, variables);

            var response = await executor.ExecuteAsync(request);

            var json = JsonConvert.SerializeObject(response);
            var jObj = JObject.Parse(json);

            var quantity =
                from p in jObj["Data"]["invoice"]["invoiceLines"]
                select (int)p["quantity"];

            var answer = quantity.Sum();

            Assert.Equal(0, response.Errors.Count);
            Assert.Equal(4, answer);
        }

        /// <summary>
        /// 10. Looking at the InvoiceLine table, provide a query that COUNTs the number of line items for each Invoice. HINT: GROUP BY
        /// </summary>
        /// Needs work
        [Fact]
        public async void Test_10()
        {
            string gql =
                "query{ " +
                    "invoices{ " +
                        "invoiceLines{ " +
                            "quantity" +
                "}}}";


            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var response = await executor.ExecuteAsync(request);

            var json = JsonConvert.SerializeObject(response);
            var jObj = JObject.Parse(json);

            var quantity = from p in jObj["Data"]["invoices"].SelectMany(i => i["invoiceLines"])
                       select (int)p["quantity"];

            var answer = quantity.Sum();

            Assert.Equal(0, response.Errors.Count);
            Assert.Equal(251, answer);
        }
        /// <summary>
        /// 11. Provide a query that includes the track name with each invoice line item.
        /// </summary>
        /// Needs work
        [Fact]
        public async void Test_11()
        {
            string gql =
                "query{" +
                    "invoiceLines{" +
                        "track{" +
                            "name" +
                "}}}";


            var serviceProvider = ComponentFactory.GetServiceProvider();
            var executor = ComponentFactory.GetQueryExecutor();
            var request = ComponentFactory.GetQueryRequest(serviceProvider, gql);

            var response = await executor.ExecuteAsync(request);

            var json = JsonConvert.SerializeObject(response);
            var jObj = JObject.Parse(json);

            var result = from p in jObj["Data"]["invoiceLines"].Select(i => i["track"])
                           select (string)p["name"];

            Assert.Equal(0, response.Errors.Count);
        }

    }
}
