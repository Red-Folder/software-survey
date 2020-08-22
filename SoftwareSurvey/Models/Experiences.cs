using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class Experiences: IStateObject
    {
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "returnOnInvestment")]
        public int? ReturnOnInvestment { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "keepingPace")]
        public int? KeepingPace { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "recruitment")]
        public int? Recruitment { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "retention")]
        public int? Retention { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "quality")]
        public int? Quality { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "predicability")]
        public int? Predicability { get; set; }
    }
}
