using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nod.Bll.Interfaces;
using Nod.Bll.Services;
using Nod.Dal;
using Nod.Dal.Interfaces;
using Nod.Dal.Repositories;
using Nod.Model;
using Nod.Model.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nod.Tcp.Api
{
    public static class Startup
    {
        public static IConfiguration BuildConfiguration()
        {
            // Build Configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            return configuration;
        }

        public static ServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            
            string conectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            string database = configuration.GetSection("MongoConnection:NodDeviceDatabase").Value;

            // DI - TO DO if sql using
            //services.AddDbContext<>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.Configure<Settings>(options =>
            {
                options.ConnectionString = conectionString;
                options.Database = database;
            });

            // Context
            services.AddTransient<IDeviceContext, DeviceContext>();

            // Services
            services.AddTransient<IDeviceDataInsertionService, DeviceDataInsertionService>();

            var serviceProvider = services.BuildServiceProvider();
            // if database creation needed
            //var context = serviceProvider.GetService<SnapshotContext>();
            //context.Database.EnsureCreated();

            return serviceProvider;
        }
    }
}
