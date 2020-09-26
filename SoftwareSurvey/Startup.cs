using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using SoftwareSurvey.Wrappers;
using System.Collections.Generic;

namespace SoftwareSurvey
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSignalR().AddAzureSignalR();

            services.AddSingleton(x => Configuration.GetSection(PersistanceConfiguration.Section).Get<PersistanceConfiguration>());

            services.AddScoped(x => new SurveyResponse());
            services.AddTransient(provider => provider.GetService<SurveyResponse>().Demographic);
            services.AddTransient(provider => provider.GetService<SurveyResponse>().SoftwareTypes);
            services.AddTransient(provider => provider.GetService<SurveyResponse>().Experiences);
            services.AddTransient(provider => provider.GetService<SurveyResponse>().OneChange);
            services.AddTransient(provider => provider.GetService<SurveyResponse>().Contact);

            //services.AddTransient<IPersistanceManager, FakePersistanceManager>();
            services.AddTransient<IPersistanceManager, CosmosDbPersistanceManager>();
            services.AddTransient<IEventLoggingService, EventLoggingService>();
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);

            services.AddTransient<INavigationManager, NavigationManagerWrapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
