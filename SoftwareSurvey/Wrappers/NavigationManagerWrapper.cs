using Microsoft.AspNetCore.Components;

namespace SoftwareSurvey.Wrappers
{
    public class NavigationManagerWrapper : INavigationManager
    {
        private readonly NavigationManager NavigationManager;

        public NavigationManagerWrapper(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        public void NavigateTo(string uri) => NavigationManager.NavigateTo(uri);

        public string Uri => NavigationManager.Uri;
    }
}
