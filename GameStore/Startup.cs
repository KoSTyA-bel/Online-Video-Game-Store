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

            // ��������� � ������ ���� ������ �������������.
            services.AddDbContext<UserContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UsersData;Trusted_Connection=True;"));

            services.AddDbContext<ProductContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Products;Trusted_Connection=True;"));

            // ��������� ������ ��� ������ � ��������������.
            services.AddTransient<IUserService, UserService>();

            // ��������� ��������� ���������(����������� ���������).
            services.AddTransient<IReport, EnglishReport>();
            services.AddTransient<AccountValidator>();

            // ��������� ������������ �����������.
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => //CookieAuthenticationOptions
            {
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    // ��������������
            app.UseAuthorization();     // �����������

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
