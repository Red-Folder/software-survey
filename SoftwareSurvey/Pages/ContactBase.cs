using SoftwareSurvey.Components;
using System.Threading.Tasks;

namespace SoftwareSurvey.Pages
{
    public class ContactBase : SurveyFormBase<Models.Contact>
    {
        protected override void OnInitialized()
        {
            TrackEvent("Loaded contact page");
            PreviousUrl = "/OneChange";
            NextUrl = "/ThankYou";
            base.OnInitialized();
        }
    }
}
