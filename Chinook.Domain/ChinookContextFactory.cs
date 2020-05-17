using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.Domain
{
    public class ChinookContextFactory : IDesignTimeDbContextFactory<ChinookContext>
    {
        public ChinookContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
