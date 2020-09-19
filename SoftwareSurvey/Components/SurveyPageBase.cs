using Microsoft.AspNetCore.Components;
using SoftwareSurvey.Services;

namespace SoftwareSurvey.Components
{
    public class SurveyPageBase: ComponentBase
    {
        [Inject]
        private IEventLoggingService _eventLoggingService { get; set; }

        protected void TrackEvent(string eventName)
        {
            _eventLoggingService.TrackEvent(eventName);
        }
    }
}

