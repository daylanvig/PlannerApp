using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Server.Data
{
    public class PlannerContextFactory : IDesignTimeDbContextFactory<PlannerContext>
    {
        IConfiguration GetAppConfiguration()
        {
            var environmentName =
                      Environment.GetEnvironmentVariable(
                          "ASPNETCORE_ENVIRONMENT");

            var dir = Directory.GetParent(AppContext.BaseDirectory);
            do
                dir = dir.Parent;
            while (dir.Name != "bin");
            dir = dir.Parent;
            var path = dir.FullName;

            var builder = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", true)
                    .AddEnvironmentVariables();

            return builder.Build();
        }

        public PlannerContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<PlannerContext>();
            options.UseMySql(GetAppConfiguration().GetConnectionString("UserContext"));
            return new PlannerContext(options.Options, null);
        }
    }
}
