using Microsoft.AspNetCore.Components;

namespace SoftwareSurvey.Services
{
    public class NavigationManagerWrapper : INavigationManagerWrapper
    {
        private readonly NavigationManager _navigationManager;

        public NavigationManagerWrapper(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void NavigateTo(string uri)
        {
            _navigationManager.NavigateTo(uri);
        }

        public string CurrentPath => _navigationManager.ToBaseRelativePath(_navigationManager.Uri);
    }
}
