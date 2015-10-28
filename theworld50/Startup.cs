using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;


namespace theworld50
{
    public class Startup
    {

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
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
