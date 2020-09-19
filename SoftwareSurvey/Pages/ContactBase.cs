using SoftwareSurvey.Components;

namespace SoftwareSurvey.Pages
{
    public class ContactBase : SurveyFormBase<Models.Contact>
    {
        protected override void OnInitialized()
        {
            TrackEvent("Loaded contact page");
            base.OnInitialized();
        }
    }
}
