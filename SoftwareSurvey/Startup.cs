using Microsoft.AspNetCore.Builder;
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

            services.AddScoped<IStateService, StateService>();
            services.AddTransient<INavigationManagerWrapper, NavigationManagerWrapper>();
            services.AddTransient<ISurveyNavigationService, SurveyNavigationService>();
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
                var thankYou = new Step
                {
                    Path = "ThankYou",
                    Name = "Thank You"
                };
                start.NextStep = demographic;
                demographic.PreviousStep = start;
                demographic.NextStep = thankYou;

                return new Steps(new List<Step>
                {
                    start,
                    demographic,
                    thankYou
                });
            });
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

            app.UseHttpsRedirection();
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
