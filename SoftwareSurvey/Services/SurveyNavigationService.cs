using SoftwareSurvey.Models;
using System.Collections.ObjectModel;

namespace SoftwareSurvey.Services
{
    public class SurveyNavigationService : ISurveyNavigationService
    {
        private readonly INavigationManagerWrapper _navigationManager;
        private readonly ISteps _steps;

        public SurveyNavigationService(ISteps steps, INavigationManagerWrapper navigationManager)
        {
            _steps = steps;
            _navigationManager = navigationManager;
        }

        public void HandleNext()
        {
            var currentPath = _navigationManager.CurrentPath;
            var nextPath = _steps.NextPath(currentPath);
            if (nextPath != null)
            {
                _navigationManager.NavigateTo(nextPath);
            }
        }

        public void HandlePrevious()
        {
            var currentPath = _navigationManager.CurrentPath;
            var previousPath = _steps.PreviousPath(currentPath);
            if (previousPath != null)
            {
                _navigationManager.NavigateTo(previousPath);
            }
        }

        public string CurrentPageTitle
        {
            get
            {
                var currentPath = _navigationManager.CurrentPath;
                return _steps.PathTitle(currentPath);
            }
        }

        public bool HasNext => _steps.HasNext(_navigationManager.CurrentPath);
        public bool HasPrevious => _steps.HasPrevious(_navigationManager.CurrentPath);

        public int CurrentPageNumber => _steps.StepNumber(_navigationManager.CurrentPath);

        public int PageCount => _steps.StepCount();

        public ReadOnlyCollection<NavigationSummary> NavigationSummaries => _steps.NavigationSummaries(_navigationManager.CurrentPath);
    }
}
