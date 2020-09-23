using System;
using ChemStoreWebApp.Areas.Identity.Data;
using ChemStoreWebApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ChemStoreWebApp.Areas.Identity.IdentityHostingStartup))]
namespace ChemStoreWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ChemStoreWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ChemStoreWebAppContextConnection")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ChemStoreWebAppContext>();
            });
        }
    }
}