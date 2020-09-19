using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SoftwareSurvey.Services;
using System.Threading.Tasks;

namespace SoftwareSurvey.Components
{
    public class SurveyPageBase<T>: ComponentBase
    {
        [Inject]
        private IEventLoggingService _eventLoggingService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        protected T Model { get; set; }

        protected string PreviousUrl;
        protected string NextUrl;

        protected void TrackEvent(string eventName)
        {
            _eventLoggingService.TrackEvent(eventName);
        }

        protected bool HasNext => !string.IsNullOrWhiteSpace(NextUrl);
        protected bool HasPrevious => !string.IsNullOrWhiteSpace(PreviousUrl);

        protected void HandleNext()
        {
            if (HasNext)
            {
                TrackEvent("Next button pressed");
                if (!IsOkToLeavePage())
                {
                    TrackEvent("Next not actioned due to invalid model");
                    return;
                }
                TrackEvent("Next actioned");
                NavigationManager.NavigateTo(NextUrl);
            }
        }

        protected void HandlePrevious()
        {
            if (HasPrevious)
            {
                TrackEvent("Previous button pressed");
                if (!IsOkToLeavePage())
                {
                    TrackEvent("Previous not actioned due to invalid model");
                    return;
                }
                TrackEvent("Previous actioned");
                NavigationManager.NavigateTo(PreviousUrl);
            }
        }

        protected virtual bool IsOkToLeavePage()
        {
            return true;
        }
    }
}

