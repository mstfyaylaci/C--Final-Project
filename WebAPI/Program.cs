using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependecyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) // ileride Autofac kullanılmak istenmesse değiştirilecek tek yer
                .ConfigureContainer<ContainerBuilder>(buider =>
                {
                    buider.RegisterModule(new AutofacBusinessModule()); // ileride Autofac kullanılmak istenmesse değiştirilecek tek yer
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
