using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Truck.Management.Test.Infra.Data.Context;

namespace Truck.Management.Test.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var hostBuilder = CreateHostBuilder(args).Build();
                using (var scope = hostBuilder.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetService<DataBaseContext>();
                    db.Database.Migrate();
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Starting Api service!", Console.ForegroundColor);

                hostBuilder.Run();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Host Error! {ex.Message}", Console.ForegroundColor);
            }


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
