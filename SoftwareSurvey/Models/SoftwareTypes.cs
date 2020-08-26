using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class SoftwareTypes: IStateObject
    {
        [DisplayName("eCommerce")]
        [Description("Website(s) selling products or services")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "eCommerce")]
        public int? ECommerce { get; set; }

        [DisplayName("Information Website")]
        [Description("Website(s) providing information (portfolio, blog, etc)")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "informationWebsite")]
        public int? InformatitonWebsite { get; set; }

        [DisplayName("Mobile App")]
        [Description("Application(s) for install on mobile or tablet")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "mobileApps")]
        public int? MobileApps { get; set; }

        [DisplayName("Line Of Business")]
        [Description("Application(s) to support specific business process")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "lineOfBusiness")]
        public int? LineOfBusiness { get; set; }

        [DisplayName("SaaS")]
        [Description("Software as a Service - sold as a product (normally subscription)")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "softwareAsAService")]
        public int? SoftwareAsAService { get; set; }

        [DisplayName("Other")]
        [Description("Any other type of software not listed above")]
        [Required(ErrorMessage = "Please select a value")]
        [JsonProperty(PropertyName = "other")]
        public int? Other { get; set; }
    }
}
