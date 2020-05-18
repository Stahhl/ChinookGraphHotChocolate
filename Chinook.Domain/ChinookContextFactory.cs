using Chinook.Common;
using Microsoft.EntityFrameworkCore;
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
            var optionsBuilder = new DbContextOptionsBuilder<ChinookContext>();
            optionsBuilder.UseSqlServer(AppSettingsManager.Settings["ConnectionStrings:Chinook"]);

            return new ChinookContext(optionsBuilder.Options);
        }
    }
}
