using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMVC.Data;
using BookStoreMVC.Models;
using BookStoreMVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreMVC
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
            services.AddDbContext<AppDbContext>(x => x.UseNpgsql(Configuration.GetConnectionString("Npgsql")));
            services.AddIdentity<User,IdentityRole<int>>(option =>
                {
                    option.User.RequireUniqueEmail = true;
                    option.Password.RequireDigit = false;
                    option.Password.RequireLowercase = false;
                    option.Password.RequiredLength = 4;
                    option.Password.RequiredUniqueChars = 0;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Lockout.AllowedForNewUsers = false;
                    option.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 1, 0);
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthentication(CookieAuthenticationDefaults.LoginPath)
                .AddCookie(opt => { opt.LoginPath = new PathString("/Account/Login"); })
                .AddGoogle(options =>
                {
                    options.ClientId = "575272670884-1m0in1jjkl59hq7utidpo3feo0agg0up.apps.googleusercontent.com";
                    options.ClientSecret = "DBocbmt-XuOecdhDBVdV3v5j";
                });
            services.AddAuthorization();
            services.AddScoped<AccountService>();
            services.AddScoped<AdService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute("404", new {controller = "Home", action = "Error"});
            });
        }
    }
}