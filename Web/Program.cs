using System;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            using(IServiceScope service = host.Services.CreateScope())
            {
                IServiceProvider provider = service.ServiceProvider;
                UserManager<UserEntity> userManager = provider.GetRequiredService<UserManager<UserEntity>>();
                RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                DataContext dataContext = provider.GetRequiredService<DataContext>();
                IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

                DefaultUser.AddDataUser(dataContext, userManager, roleManager, configuration).Wait();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
            webBuilder.UseStartup<Startup>();
            });
        }
    }
}
