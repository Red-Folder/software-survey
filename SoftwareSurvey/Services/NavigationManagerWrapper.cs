using Microsoft.AspNetCore.Components;
using System;

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

        public string CurrentPath
        {
            get
            {
                var currentFullUrl = new Uri(_navigationManager.Uri);
                return currentFullUrl.AbsolutePath;
            }
        }
            
    }
}
