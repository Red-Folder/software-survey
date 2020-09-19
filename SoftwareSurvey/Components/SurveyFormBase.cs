using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SoftwareSurvey.Models;

namespace SoftwareSurvey.Components
{
    public class SurveyFormBase<T> : SurveyPageBase where T : StateObject
    {
        protected EditContext EditContext;

        [Inject]
        protected T Model { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(Model);
        }
    }
}
