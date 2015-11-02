using AutoMapper;
using Microsoft.AspNet.Builder;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Newtonsoft.Json.Serialization;
using theworld50.Models;
using theworld50.Services;
using theworld50.ViewModels;

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
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddScoped<IMailService, DebugMailService>();

            services.AddTransient<WorldContextSeedData>();
            services.AddScoped<IWorldRepository, WorldRepository>();

            services.AddLogging();

            services.AddTransient<CoordService>();

            services.AddEntityFramework()
                         .AddSqlServer()
                         .AddDbContext<WorldContext>();
        }

        public void Configure(IApplicationBuilder app, WorldContextSeedData wcSeedData, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Debug);

            Mapper.Initialize(config =>
            {
                config.CreateMap<Trip, TripViewModel>().ReverseMap();
                config.CreateMap<Stop, StopViewModel>().ReverseMap();
            });

            app.UseStaticFiles();
            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            app.UseMvc(config => config.MapRoute("Default", "{controller}/{action}/{id?}", new {controller = "App", action = "Index"}));

            wcSeedData.EnsureSeedData();
        }
    }
}
