using System;
using System.Configuration;
using ChemStoreWebApp.Areas.Identity.Data;
using ChemStoreWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;



[assembly: HostingStartup(typeof(ChemStoreWebApp.Areas.Identity.IdentityHostingStartup))]
namespace ChemStoreWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public IConfiguration Configuration { get; }
        public IdentityHostingStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<chemstoreContext>();
            });
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<chemstoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ChemStoreDB")));
        }
    }
}