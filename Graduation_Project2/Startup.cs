using Graduation_Project2.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.Facebook;
//using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Graduation_Project2.Controllers;
namespace Graduation_Project2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public async void ConfigureServices(IServiceCollection services)
        {
          //  var accountController = new AccountController(/* pass required dependencies */);
          //  await accountController.CreateTheRoles();

            services.AddControllersWithViews();

            services.AddDbContext<Graduation2DbContext>(options =>
            { options.UseSqlServer(Configuration.GetConnectionString("con")); });
          

            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<Graduation2DbContext>();

            services.AddSession(x => x.IdleTimeout = TimeSpan.FromSeconds(300));
            services.AddDistributedMemoryCache();

            /*
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;

            })
        .AddCookie()
        .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
        {
            options.ClientId = Configuration["Authentication:Google:ClientId"];
            options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
        });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
            })
               .AddFacebook(options =>
               {
                   options.AppId = "132484919814503";
                   options.AppSecret = "f51cf592dc32ffc747a9e8b1e3d09800";
               });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });




            //bool isCreateRolesMethodCalled = false;

            //if (!isCreateRolesMethodCalled)
            //{
            //    using (var scope = app.ApplicationServices.CreateScope())
            //    {
            //        var serviceProvider = scope.ServiceProvider;
            //        var accountController = new AccountController(); // Create an instance of HomeController

            //        accountController.CreateTheRoles(serviceProvider); // Pass the service provider to the Seed method

            //        isCreateRolesMethodCalled = true;
            //    }
            //}

        }
    }
}
/*
  using (var scope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var homeController = new HomeController(); // Create an instance of HomeController

            homeController.Seed(serviceProvider); // Pass the service provider to the Seed method

            isSeedMethodCalled = true;
        }
 */