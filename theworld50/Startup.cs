using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using theworld50.Services;

namespace theworld50
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public Startup(IApplicationEnvironment app)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(app.ApplicationBasePath)
                            .AddEnvironmentVariables()
                            .AddJsonFile("config.json");

            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IMailService, DebugMailService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            app.UseMvc(config => config.MapRoute("Default", "{controller}/{action}/{id?}", new {controller = "App", action = "Index"}));
        }
    }
}
