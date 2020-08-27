using SoftwareSurvey.Models;
using System.Collections.ObjectModel;

namespace SoftwareSurvey.Services
{
    public interface ISurveyNavigationService
    {
        bool HasNext { get; }
        bool HasPrevious { get; }

        void HandleNext();
        void HandlePrevious();

        string CurrentPageTitle { get; }

        int CurrentPageNumber { get; }
        int PageCount { get; }
    }
}