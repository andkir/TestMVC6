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
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Diagnostics;

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
                            .AddJsonFile("config.json")
                            .AddUserSecrets();

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

            services.AddIdentity<WorldUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<WorldContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Cookies.ApplicationCookie.LoginPath = new Microsoft.AspNet.Http.PathString("/Auth/Login");
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

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, WorldContextSeedData wcSeedData, ILoggerFactory loggerFactory)
        {

            var password = Configuration.Get<string>("password");
            loggerFactory.AddDebug(LogLevel.Debug);

            Mapper.Initialize(config =>
            {
                config.CreateMap<Trip, TripViewModel>().ReverseMap();
                config.CreateMap<Stop, StopViewModel>().ReverseMap();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              //  app.UseRuntimeInfoPage(); // default path is /runtimeinfo
              //  app.UseExceptionHandler("/app/error");

            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            app.UseMvc(config => config.MapRoute("Default", "{controller=App}/{action=Index}/{id?}"));

            await wcSeedData.EnsureSeedDataAsync();
        }
    }
}
