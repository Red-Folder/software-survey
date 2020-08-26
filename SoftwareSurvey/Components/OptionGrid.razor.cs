using Microsoft.AspNetCore.Components;
using SoftwareSurvey.Models;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareSurvey.Components
{
    public partial class OptionGrid<TValue>
    {
        [Parameter]
        public TValue Model { get; set; }
        [Parameter]
        public string NotApplicableLabel { get; set; }

        private List<Option> _options;

        protected override void OnParametersSet()
        {
            _options = Model.GetType().GetProperties().Select(x => new Option(this, Model, x)).ToList();
        }
    }
}
