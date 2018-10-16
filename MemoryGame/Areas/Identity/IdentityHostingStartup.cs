using System;
using MemoryGame.Areas.Identity.Data;
using MemoryGame.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(MemoryGame.Areas.Identity.IdentityHostingStartup))]
namespace MemoryGame.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MemoryGameContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MemoryGameContextConnection")));
                //Email confirmation
                services.AddDefaultIdentity<User>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<MemoryGameContext>();
            });
        }
    }
}