using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using System.Threading.Tasks;

namespace SoftwareSurvey.Components
{
    public partial class SurveyNavigation<T> where T: IStateObject, new()
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        private ISurveyNavigationService _surveyNavigationService { get; set; }

        [Inject]
        private IStateService _stateService { get; set; }

        public T Model;
        private EditContext _editContext;

        protected override void OnInitialized()
        {
            Model = _stateService.GetOrNew<T>();
            _editContext = new EditContext(Model);
       }

        private void HandleNext()
        {
            if (IsValid && _surveyNavigationService.HasNext)
            {
                _stateService.Save(Model);
                _surveyNavigationService.HandleNext();
            }
        }

        private void HandlePrevious()
        {
            if (IsValid && _surveyNavigationService.HasPrevious)
            {
                _stateService.Save(Model);
                _surveyNavigationService.HandlePrevious();
            }
        }

        private bool IsValid => _editContext.Validate();
        private bool HasNext => _surveyNavigationService.HasNext;
        private bool HasPrevious => _surveyNavigationService.HasPrevious;
    }
}
