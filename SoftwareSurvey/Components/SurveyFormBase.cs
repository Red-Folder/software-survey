using Microsoft.AspNetCore.Components.Forms;
using SoftwareSurvey.Models;

namespace SoftwareSurvey.Components
{
    public class SurveyFormBase<T> : SurveyPageBase<T> where T : StateObject
    {
        protected EditContext EditContext;

        protected override void OnInitialized()
        {
            EditContext = new EditContext(Model);
        }

        protected override bool IsOkToLeavePage()
        {
            if (!EditContext.Validate()) return false;

            if (!Model.IsValid()) return false;

            return true;
        }
    }
}
