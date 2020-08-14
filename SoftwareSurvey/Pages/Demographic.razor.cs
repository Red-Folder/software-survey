using Microsoft.AspNetCore.Components;
using SoftwareSurvey.Services;
using System.Threading.Tasks;

namespace SoftwareSurvey.Pages
{
    public partial class Demographic
    {
        protected Models.Demographic Model;

        [Inject]
        protected IStateService _stateService { get; set; }

        protected override void OnInitialized()
        {
            Model = _stateService.GetOrNew<Models.Demographic>();
        }

        protected Task Save()
        {
            _stateService.Save(Model);
            return Task.CompletedTask;
        }
    }
}
