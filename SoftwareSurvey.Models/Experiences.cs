using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class Experiences
    {
        [DisplayName("ROI")]
        [Description("Is it providing Return On Investment?")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "returnOnInvestment")]
        public int? ReturnOnInvestment { get; set; }

        [DisplayName("Keeping pace")]
        [Description("Is it keeping pace with the business needs?")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "keepingPace")]
        public int? KeepingPace { get; set; }

        [DisplayName("Recruitment")]
        [Description("It is easy to recruit the developers you want?")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "recruitment")]
        public int? Recruitment { get; set; }

        [DisplayName("Retention")]
        [Description("Is it easy to retain your developers?")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "retention")]
        public int? Retention { get; set; }

        [DisplayName("Quality")]
        [Description("Is the software of a high quality?")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "quality")]
        public int? Quality { get; set; }

        [DisplayName("Predicability")]
        [Description("Is the software development predicatable?")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "predicability")]
        public int? Predicability { get; set; }
    }
}
