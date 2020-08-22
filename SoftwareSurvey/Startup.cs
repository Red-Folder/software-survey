using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using System;
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

            services.AddSingleton(x => Configuration.GetSection(PersistanceConfiguration.Section).Get<PersistanceConfiguration>());
            services.AddSingleton<ISteps>(x =>
            {
                var start = new Step
                {
                    Path = "",
                    Name = "Start"
                };
                var demographic = new Step
                {
                    Path = "Demographic",
                    Name = "Demographic"
                };
                var softwareTypes = new Step
                {
                    Path = "SoftwareTypes",
                    Name = "Software Types"
                };
                var experiences = new Step
                {
                    Path = "Experiences",
                    Name = "Your Experiences"
                };
                var thankYou = new Step
                {
                    Path = "ThankYou",
                    Name = "Thank You"
                };
                start.NextStep = demographic;
                demographic.PreviousStep = start;
                demographic.NextStep = softwareTypes;
                softwareTypes.PreviousStep = demographic;
                softwareTypes.NextStep = experiences;
                experiences.PreviousStep = softwareTypes;
                experiences.NextStep = thankYou;

                return new Steps(new List<Step>
                {
                    start,
                    demographic,
                    softwareTypes,
                    experiences,
                    thankYou
                });
            });

            services.AddScoped(x => new SurveyResponse
            {
                Id = Guid.NewGuid().ToString(),
                Year = DateTime.Now.Year,
                CreatedTimestamp = DateTime.Now,
                Demographic = new Demographic(),
                SoftwareTypes = new SoftwareTypes(),
                Experiences = new Experiences()
            });

            services.AddTransient<INavigationManagerWrapper, NavigationManagerWrapper>();
            services.AddTransient<ISurveyNavigationService, SurveyNavigationService>();
            //services.AddTransient<IPersistanceManager, FakePersistanceManager>();
            services.AddTransient<IPersistanceManager, CosmosDbPersistanceManager>();
            services.AddApplicationInsightsTelemetry(Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);
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
