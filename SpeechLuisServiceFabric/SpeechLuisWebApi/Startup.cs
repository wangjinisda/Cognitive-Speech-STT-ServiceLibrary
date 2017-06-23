﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Common.Service.Model;
using SpeechLuisOwin.Src.Static;
using Common.Service.AuthorizationProvider;
using Common.Interface.IService;
using SpeechLuisOwin.Src.Services;
using SpeechLuisOwin.Src.Ext;

namespace SpeechLuisWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(LuisModel), provider => {
                return new LuisModel
                {
                    LuisAppId = Configurations.luisAppId,
                    LuisSubKey = Configurations.luisSubKey
                };
            });

            services.AddTransient(typeof(SpeechModel), provider => {
                return new SpeechModel
                {
                    Locale = "zh-cn",
                    SpeechSubKey = Configurations.speechSubKey
                };
            });

            services.AddTransient(typeof(AADModel), provider => {
                return new AADModel
                {
                    AAD_Tenant = Configurations.aad_Tenant,
                    AAD_Audience = Configurations.aad_Audience,
                    AAD_ClientId = Configurations.aad_ClientId,
                    AAD_AuthUri = Configurations.aad_AuthUri,
                    AAD_Key = Configurations.aad_Key,
                    AAD_Resource = Configurations.aad_Resource
                };
            });

            services.AddTransient<AADTokenProvider>();
            services.AddSingleton(new Authentication(Configurations.speechSubKey_HK));
            services.AddSingleton<ILuisService, LuisService>();
            services.AddSingleton<ISpeechRestService, SpeechRestService>();
            services.AddSingleton<ISpeechService, SpeechService>();

            // Add framework services.
            services.AddMvc();
            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );

            app.UseAuth();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Values}/{action=Get}");
            });
        }
    }
}
