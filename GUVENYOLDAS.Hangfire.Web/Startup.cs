using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using Hangfire.MemoryStorage;
using GUVENYOLDAS.Hangfire.Web.Helper;
using GUVENYOLDAS.Hangfire.Web.Models.Base;

namespace GUVENYOLDAS.Hangfire.Web
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
            services.AddControllersWithViews();

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                //TODO : UseMemoryStorage -> UseSqlServerStorage for using database to follow.
                .UseMemoryStorage());

            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //TODO : default hangfire page is here so you added authorization now.
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });


            if (bool.Parse(Configuration["AppConfig:EnableHangfire"]))
            {
                //TODO : adding my hangfire jobs here
                SubscribeHangfireModel.SubscribeCustomHangfires(Configuration);
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}