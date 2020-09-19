using SoftwareSurvey.Components;
using System.Threading.Tasks;

namespace SoftwareSurvey.Pages
{
    public class OneChangeBase : SurveyFormBase<Models.OneChange>
    {
        protected override void OnInitialized()
        {
            TrackEvent("Loaded one change page");
            base.OnInitialized();
        }
    }
}
