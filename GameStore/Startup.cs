using GameStore.Services.Products;
using GameStore.Services.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddDbContext<IUserContext, UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Users")), ServiceLifetime.Singleton);
            services.AddDbContext<IProductContextAsync, ProductContextAsync>(options => options.UseSqlServer(Configuration.GetConnectionString("Products")), ServiceLifetime.Scoped);

            // All for user servises.
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductServiceAsync, ProductServiceAsync>();
            services.AddTransient<IProductService>(porovider => (IProductService)porovider.GetService(typeof(IProductServiceAsync)));
            services.AddTransient(provider => LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("App logger"));

            services.AddTransient<AccountValidator>();

            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                // CookieAuthenticationOptions
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            });
        }

        public void Configure(IApplicationBuilder app, ILogger logger)
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
