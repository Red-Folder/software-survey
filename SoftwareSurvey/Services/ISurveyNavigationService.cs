namespace SoftwareSurvey.Services
{
    public interface ISurveyNavigationService
    {
        bool HasNext { get; }
        bool HasPrevious { get; }

        void HandleNext();
        void HandlePrevious();

        string CurrentPageTitle();
    }
}