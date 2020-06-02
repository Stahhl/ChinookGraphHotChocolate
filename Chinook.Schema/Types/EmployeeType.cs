using Chinook.Domain;
using Chinook.Domain.Models;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Schema.Types
{
    public class EmployeeType : ObjectType<Employee>
    {
        protected override void Configure(IObjectTypeDescriptor<Employee> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Field<EmployeeType>(r => r.ReportsTo(default, default))
                .UseFiltering()
                .Name(nameof(EmployeeType.ReportsTo).ToLower());

            descriptor.Field<EmployeeType>(r => r.Customers(default, default))
                .UseFiltering()
                .Name(nameof(EmployeeType.Customers).ToLower());

            descriptor.Field<EmployeeType>(r => r.InverseReportsTo(default, default))
                .UseFiltering()
                .Name(nameof(EmployeeType.InverseReportsTo).ToLower());
        }

        public Employee ReportsTo([Service] ChinookContext context, [Parent] Employee employee)
        {
            var result = context.Employee.FirstOrDefault(e => e.EmployeeId == employee.ReportsTo);

            return result;
        }
        public IQueryable<Customer> Customers([Service] ChinookContext context, [Parent] Employee employee)
        {
            var result = context.Customer.Where(c => c.SupportRepId == employee.EmployeeId);

            return result;
        }
        public IQueryable<Employee> InverseReportsTo([Service] ChinookContext context, [Parent] Employee employee)
        {
            var result = context.Employee.Where(c => c.ReportsTo == employee.EmployeeId);

            return result;
        }
    }
}
