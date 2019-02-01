using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LuckyWheel.DAL
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            string path = Directory.GetCurrentDirectory().Replace("DAL","API");

            var builder = new DbContextOptionsBuilder<Context>();
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(@path)
              .AddJsonFile("appsettings.json")
              .Build();

            var connection = configuration["ConnectionStrings:DefaultConnection"];

            builder.UseSqlServer(connection);

            return new Context(builder.Options);
        }
    }
}
