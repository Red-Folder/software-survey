using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
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
            services.AddSingleton<ISteps>(x =>
            {
                var start = new Step
                {
                    Path = "/",
                    PageTitle = "WELCOME"
                };
                var demographic = new Step
                {
                    Path = "/Demographic",
                    PageTitle = "DEMOGRAPHICS"
                };
                var softwareTypes = new Step
                {
                    Path = "/SoftwareTypes",
                    PageTitle = "SOFTWARE TYPES"
                };
                var experiences = new Step
                {
                    Path = "/Experiences",
                    PageTitle = "YOUR EXPERIENCES"
                };
                var oneChange = new Step
                {
                    Path = "/OneChange",
                    PageTitle = "ONE CHANGE"
                };
                var contact = new Step
                {
                    Path = "/Contact",
                    PageTitle = "FURTHER CONTACT"
                };
                var thankYou = new Step
                {
                    Path = "/ThankYou",
                    PageTitle = "THANK YOU"
                };
                start.NextStep = demographic;
                demographic.PreviousStep = start;
                demographic.NextStep = softwareTypes;
                softwareTypes.PreviousStep = demographic;
                softwareTypes.NextStep = experiences;
                experiences.PreviousStep = softwareTypes;
                experiences.NextStep = oneChange;
                oneChange.PreviousStep = experiences;
                oneChange.NextStep = contact;
                contact.PreviousStep = oneChange;
                contact.NextStep = thankYou;

                return new Steps(new List<Step>
                {
                    start,
                    demographic,
                    softwareTypes,
                    experiences,
                    oneChange,
                    contact,
                    thankYou
                });
            });

            services.AddScoped(x => new SurveyResponse());

            services.AddTransient<INavigationManagerWrapper, NavigationManagerWrapper>();
            services.AddTransient<ISurveyNavigationService, SurveyNavigationService>();
            //services.AddTransient<IPersistanceManager, FakePersistanceManager>();
            services.AddTransient<IPersistanceManager, CosmosDbPersistanceManager>();
            services.AddTransient<IEventLoggingService, EventLoggingService>();
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
