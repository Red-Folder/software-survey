using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using SoftwareSurvey.Models;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareSurvey.Components
{
    public partial class OptionGrid<TValue>
    {
        [CascadingParameter]
        public EditContext EditContext { get; set; }

        [Parameter]
        public string NotApplicableLabel { get; set; }

        private List<Option> _options;

        private TValue Model { get; set; }

        protected override void OnParametersSet()
        {
            Model = (TValue)this.EditContext.Model;
            _options = Model.GetType().GetProperties().Select(x => new Option(this, Model, x)).ToList();
        }
    }
}
