using SoftwareSurvey.Components;
using SoftwareSurvey.Models;

namespace SoftwareSurvey.Pages
{
    public class IndexBase: SurveyPageBase<SurveyResponse>
    {
        protected override void OnInitialized()
        {
            Model.HasBeenToStart = true;
            NextUrl = "/Demographic";
            TrackEvent("Loaded index page");
        }
    }
}
