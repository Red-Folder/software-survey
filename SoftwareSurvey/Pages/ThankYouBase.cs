using Microsoft.AspNetCore.Components;
using SoftwareSurvey.Components;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using System;
using System.Threading.Tasks;

namespace SoftwareSurvey.Pages
{
    public class ThankYouBase : SurveyPageBase<SurveyResponse>
    {
        [Inject]
        private IPersistanceManager _persistanceManager { get; set; }

        protected bool Saved { get; set; }
        protected bool Error { get; set; }

        protected override void OnInitialized()
        {
            TrackEvent("Loaded thank you page");
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SaveData();
            }
        }

        protected void HandleRetry(EventArgs eventArgs)
        {
            Saved = false;
            Error = false;
            StateHasChanged();

            SaveData();
        }

        private void SaveData()
        {
            TrackEvent("Save started");

            var task = _persistanceManager.Persist(Model);
            task.ContinueWith(async x =>
            {
                Error = !x.Result;
                Saved = true;
                await InvokeAsync(StateHasChanged);

                if (Error)
                {
                    TrackEvent("Save failed");
                }
                else
                {
                    TrackEvent("Save completed");
                }
            });

            Task.Run(() => task);
        }
    }
}
