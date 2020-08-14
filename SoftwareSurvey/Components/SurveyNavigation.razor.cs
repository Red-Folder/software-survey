using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SoftwareSurvey.Services;
using System.Threading.Tasks;

namespace SoftwareSurvey.Components
{
    public partial class SurveyNavigation
    {
        [Parameter]
        public object Model { get; set; }

        [Parameter]
        public EventCallback Save { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        private ISurveyNavigationService _surveyNavigationService { get; set; }

        private EditContext _editContext;

        protected override void OnInitialized()
        {
            if (Model == null) Model = new { };
            _editContext = new EditContext(Model);
       }

        private async Task HandleNext()
        {
            if (IsValid && _surveyNavigationService.HasNext)
            {
                await Save.InvokeAsync(null);
                _surveyNavigationService.HandleNext();
            }
        }

        private async Task HandlePrevious()
        {
            if (IsValid && _surveyNavigationService.HasPrevious)
            {
                await Save.InvokeAsync(null);
                _surveyNavigationService.HandlePrevious();
            }
        }

        private bool IsValid => _editContext.Validate();
        private bool HasNext => _surveyNavigationService.HasNext;
        private bool HasPrevious => _surveyNavigationService.HasPrevious;
    }
}
