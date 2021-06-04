using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrzepisyWeb.Areas.Identity.Data;
using PrzepisyWeb.Data;

[assembly: HostingStartup(typeof(PrzepisyWeb.Areas.Identity.IdentityHostingStartup))]
namespace PrzepisyWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
    /*        builder.ConfigureServices((context, services) => {
            services.AddDbContext<RecipeContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("RecipeDB")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<RecipeContext>();
            });
        */}
    }
}