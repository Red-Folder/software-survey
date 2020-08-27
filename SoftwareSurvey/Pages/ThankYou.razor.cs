using Microsoft.AspNetCore.Components;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using System;
using System.Threading.Tasks;

namespace SoftwareSurvey.Pages
{
    public partial class ThankYou
    {
        [Inject]
        private SurveyResponse _surveyResponse { get; set; }

        [Inject]
        private IPersistanceManager _persistanceManager { get; set; }

        private bool Saved { get; set; }
        private bool Error { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                SaveData();
            }
        }

        private void HandleRetry(EventArgs eventArgs)
        {
            Saved = false;
            Error = false;
            StateHasChanged();

            SaveData();
        }

        private void SaveData()
        {
            var task = _persistanceManager.Persist(_surveyResponse);
            task.ContinueWith(async x =>
            {
                Error = !x.Result;
                Saved = true;
                await InvokeAsync(StateHasChanged);
            });

            Task.Run(() => task);
        }
    }
}
