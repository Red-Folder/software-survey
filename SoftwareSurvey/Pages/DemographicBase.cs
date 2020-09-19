using SoftwareSurvey.Components;
using System.Threading.Tasks;

namespace SoftwareSurvey.Pages
{
    public class DemographicBase : SurveyFormBase<Models.Demographic>
    {
        protected override void OnInitialized()
        {
            TrackEvent("Loaded demographic page");
            PreviousUrl = "/";
            NextUrl = "/SoftwareTypes";
            base.OnInitialized();
        }
    }
}
