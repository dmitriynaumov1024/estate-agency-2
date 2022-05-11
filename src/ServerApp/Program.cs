using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ServerApp
{
    public class Program
    {
        public static void Main (string[] args)
        {
            EAProvider.Init();

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(withMyStartup)
                .Build().Run();

            Console.Write("EAProvider is being shut down...\n");
            EAProvider.Shutdown();
            Console.Write("EAProvider was shut down successfully.\n");
        }

        static void withMyStartup (IWebHostBuilder webBuilder) {
            webBuilder.UseStartup<Startup>();
        }
    }
}
