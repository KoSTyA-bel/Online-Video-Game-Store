using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GameStore.Models.Users;
using GameStore.Models;
using Microsoft.Extensions.Logging;
using System.Configuration;

namespace GameStore
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
            services.AddControllersWithViews();

            // Databases.
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Users")));
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Products")));

            // All for user servises.
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ILogger>(config => LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("App logger"));

            services.AddTransient<AccountValidator>();

            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => //CookieAuthenticationOptions
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {
            logger.LogInformation("App starting");

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    
            
            app.UseAuthorization();     
            
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=Index}");
            });

            logger.LogInformation($"App starterd");
        }
    }
}
