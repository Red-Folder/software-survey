using SoftwareSurvey.Components;
using System.Threading.Tasks;

namespace SoftwareSurvey.Pages
{
    public class SoftwareTypesBase : SurveyFormBase<Models.SoftwareTypes>
    {
        protected override void OnInitialized()
        {
            TrackEvent("Loaded software types page");
            base.OnInitialized();
        }
    }
}
