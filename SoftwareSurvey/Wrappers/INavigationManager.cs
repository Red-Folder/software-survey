namespace SoftwareSurvey.Wrappers
{
    public interface INavigationManager
    {
        string Uri { get; }

        void NavigateTo(string uri);
    }
}