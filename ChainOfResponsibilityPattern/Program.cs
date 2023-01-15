using ChainOfResponsibilityPattern.DAL;
using ChainOfResponsibilityPattern.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var identiyDbContext = scope.ServiceProvider.GetRequiredService<Context>();
            //identiyDbContext.Database.Migrate();
            Enumerable.Range(1, 20).ToList().ForEach(x =>
             {
                 identiyDbContext.Products.Add(new Product { Name = $"kalem{x}", Price = 100, Stock = 200 });
             });

            identiyDbContext.SaveChanges();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
