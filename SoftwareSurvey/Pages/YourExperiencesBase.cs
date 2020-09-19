using SoftwareSurvey.Components;

namespace SoftwareSurvey.Pages
{
    public class YourExperiencesBase : SurveyFormBase<Models.Experiences>
    {
        protected override void OnInitialized()
        {
            TrackEvent("Loaded your experiences page");
            PreviousUrl = "/SoftwareTypes";
            NextUrl = "/OneChange";
            base.OnInitialized();
        }
    }
}
