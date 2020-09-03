namespace SoftwareSurvey.Services
{
    public interface IEventLoggingService
    {
        void TrackEvent(string eventName);
    }
}