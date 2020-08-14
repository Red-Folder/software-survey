namespace SoftwareSurvey.Services
{
    public interface INavigationManagerWrapper
    {
        void NavigateTo(string uri);
        string CurrentPath { get; }
    }
}