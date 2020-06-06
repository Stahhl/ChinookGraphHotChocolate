using Chinook.Domain;
using Chinook.Domain.Models;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Schema.Querys
{
    public class Query
    {
        #region Customer
        public Customer Customer([Service] ChinookContext context, int id)
        {
            var result = context.Customer.FirstOrDefault(c => c.CustomerId == id);

            return result;
        }
        public IQueryable<Customer> Customers([Service] ChinookContext context)
        {
            var result = context.Customer;

            return result;
        }
        #endregion

        #region Artist
        public Artist Artist([Service] ChinookContext context, int id)
        {
            var result = context.Artist.FirstOrDefault(c => c.ArtistId == id);

            return result;
        }
        public IQueryable<Artist> Artists([Service] ChinookContext context)
        {
            var result = context.Artist;

            return result;
        }
        #endregion

        #region Invoice
        public Invoice Invoice([Service] ChinookContext context, int id)
        {
            var result = context.Invoice.FirstOrDefault(c => c.InvoiceId == id);

            return result;
        }
        public IQueryable<Invoice> Invoices([Service] ChinookContext context)
        {
            var result = context.Invoice;

            return result;
        }
        #endregion

        #region InvoiceLine
        public InvoiceLine InvoiceLine([Service] ChinookContext context, int id)
        {
            var result = context.InvoiceLine.FirstOrDefault(c => c.InvoiceLineId == id);

            return result;
        }
        public IQueryable<InvoiceLine> InvoiceLines([Service] ChinookContext context)
        {
            var result = context.InvoiceLine;

            return result;
        }
        #endregion

        #region Employee
        public Employee Employee([Service] ChinookContext context, int id)
        {
            var result = context.Employee.FirstOrDefault(c => c.EmployeeId == id);

            return result;
        }
        public IQueryable<Employee> Employees([Service] ChinookContext context)
        {
            var result = context.Employee;

            return result;
        }
        #endregion
    }
}
