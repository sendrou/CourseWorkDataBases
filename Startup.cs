using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Cargo.Models;
using Cargo.Middleware;
using Cargo.Data;
using Microsoft.AspNetCore.Identity;

namespace Cargo
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            string? connection = Configuration.GetConnectionString("CargoContextConnection");
            services.AddDbContext<CargoContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews(options =>
            {
                options.CacheProfiles.Add("Caching",
                    new CacheProfile()
                    {
                        Duration = 296
                    });
            });

            services.AddSession();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Создание ролей "user" и "admin", если они не существуют
            CreateRoleIfNotExists(roleManager, "user").Wait();
            CreateRoleIfNotExists(roleManager, "admin").Wait();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            

            app.UseSession();

            app.UseDbInitializer();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "account",
                    pattern: "account/{action=Login}/{id?}",
                    defaults: new { controller = "Account" }
                );
            });

        }
        private async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
