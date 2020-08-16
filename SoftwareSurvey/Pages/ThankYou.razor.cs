using Microsoft.AspNetCore.Components;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
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

        protected override void OnInitialized()
        {
            var task = _persistanceManager.Persist(_surveyResponse);
            task.ContinueWith(async x =>
            {
                Saved = true;
                await InvokeAsync(StateHasChanged);
            });

            Task.Run(() => task);
        }
    }
}
