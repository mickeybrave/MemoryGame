using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemoryGame.Areas.Identity.Data;
using MemoryGame.Areas.Identity.Services;
using MemoryGame.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Infra;

namespace MemoryGame
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Email confirmation
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IRandomService, RandomService>();
            services.AddSingleton<ISessionHelper, SessionHelper>();
            
            services.Configure<AuthMessageSenderOptions>(Configuration);


            services.AddMvc()
                  .AddViewLocalization()
                  .AddDataAnnotationsLocalization()
                   .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.AddPageRoute("/Lists/Index", "");
                    });
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
