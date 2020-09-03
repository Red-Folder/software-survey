using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SoftwareSurvey.Models;
using SoftwareSurvey.Services;
using System;

namespace SoftwareSurvey.Components
{
    public partial class SurveyNavigation
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public IStateObject Model { get; set; }

        [Parameter]
        public Func<bool> IsModelValid { get; set; }

        [Inject]
        private ISurveyNavigationService _surveyNavigationService { get; set; }

        [Inject]
        private IEventLoggingService _eventLoggingService { get; set; }

        private EditContext _editContext;

        private bool HasModel => Model != null;

        protected override void OnInitialized()
        {
            if (HasModel)
            {
                _editContext = new EditContext(Model);
            }
        }

        private void HandleNext()
        {
            if (_surveyNavigationService.HasNext)
            {
                _eventLoggingService.TrackEvent("Next button pressed");
                if (ShouldNotChangePageDueToInvalidModel)
                {
                    _eventLoggingService.TrackEvent("Next not actioned due to invalid model");
                    return;
                }
                _eventLoggingService.TrackEvent("Next actioned");
                _surveyNavigationService.HandleNext();
            }
        }

        private void HandlePrevious()
        {
            if (_surveyNavigationService.HasPrevious)
            {
                _eventLoggingService.TrackEvent("Previous button pressed");
                if (ShouldNotChangePageDueToInvalidModel)
                {
                    _eventLoggingService.TrackEvent("Previous not actioned due to invalid model");
                    return;
                }
                _eventLoggingService.TrackEvent("Previous actioned");
                _surveyNavigationService.HandlePrevious();
            }
        }

        private bool ShouldNotChangePageDueToInvalidModel
        {
            get
            {
                if (!HasModel) return false;

                if (!_editContext.Validate()) return true;

                if (IsModelValid != null && !IsModelValid()) return true;

                return false;
            }
        }
        private bool HasNext => _surveyNavigationService.HasNext;
        private bool HasPrevious => _surveyNavigationService.HasPrevious;
    }
}
