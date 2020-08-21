using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class SoftwareTypes: IStateObject
    {
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "eCommerce")]
        public int? ECommerce { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "informationWebsite")]
        public int? InformatitonWebsite { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "mobileApps")]
        public int? MobileApps { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "lineOfBusiness")]
        public int? LineOfBusiness { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "softwareAsAService")]
        public int? SoftwareAsAService { get; set; }

        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "other")]
        public int? Other { get; set; }
    }
}
