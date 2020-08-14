using Microsoft.AspNetCore.Components;
using SoftwareSurvey.Services;
using System.Threading.Tasks;

namespace SoftwareSurvey.Pages
{
    public partial class ThankYou
    {
        [Inject]
        private IStateService _stateService { get; set; }

        private bool Saved { get; set; }

        protected override void OnInitialized()
        {
            var task = _stateService.Persist();
            task.ContinueWith(async x =>
            {
                Saved = true;
                await InvokeAsync(StateHasChanged);
            });

            Task.Run(() => task);
        }
    }
}
