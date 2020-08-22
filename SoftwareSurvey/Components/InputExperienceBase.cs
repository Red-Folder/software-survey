using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace SoftwareSurvey.Components
{
    public class InputExperienceBase : InputBase<int?>
    {
        [Parameter]
        public string Label { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Description { get; set; }

        protected override bool TryParseValueFromString(string value, out int? result, out string validationErrorMessage)
        {
            int temp;
            var valid = int.TryParse(value, out temp);
            result = temp;
            validationErrorMessage = valid ? "" : "Not a number";
            return valid;
        }
    }
}
