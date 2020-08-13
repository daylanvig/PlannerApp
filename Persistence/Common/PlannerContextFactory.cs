using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Persistence
{
    public class PlannerContextFactory : IDesignTimeDbContextFactory<PlannerContext>
    {
        private string GetEnvironmentName() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        IConfiguration GetAppConfiguration()
        {
            var environmentName = GetEnvironmentName();
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
            options.UseSqlServer(GetAppConfiguration().GetConnectionString("PlannerContext"));
            return new PlannerContext(options.Options, null);
        }
    }
}
