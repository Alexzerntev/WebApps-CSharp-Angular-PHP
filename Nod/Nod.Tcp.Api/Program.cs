using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nod.Bll.Interfaces;
using Nod.Bll.Services;
using System;
using System.Threading;

namespace Nod.Tcp.Api
{
    class Program
    {

        public static IConfiguration Configuration { get; set; }
        public static ServiceProvider ServiceProvider { get; set; }

        public static int Main(String[] args)
        {
            // Configuration
            Configuration = Startup.BuildConfiguration();
            // Dependency Injection
            ServiceProvider = Startup.ConfigureServices(Configuration);
            AsyncSocketListener listener = new AsyncSocketListener(ServiceProvider.GetService<IDeviceDataInsertionService>());

            int sleeptime = 0;
            DateTime started = DateTime.Now;

            while (true)
            {
                try
                {
                    started = DateTime.Now;
                    listener.StartListening();
                }
                catch (Exception e)
                {
                    if ((DateTime.Now - started).TotalHours > 1)
                    {
                        sleeptime = 0;
                    }
                    else
                    {
                        sleeptime = (sleeptime + 1000) * 2;
                    }
                    Thread.Sleep(sleeptime);

                    listener = null;
                    GC.Collect();
                    listener = new AsyncSocketListener(ServiceProvider.GetService<IDeviceDataInsertionService>());
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}