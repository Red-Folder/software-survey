using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class Demographic: IStateObject
    {
        [Required(ErrorMessage = "Please provide company size")]
        [DisplayName("Company Size")]
        [JsonProperty(PropertyName = "companySize")]
        public string CompanySize { get; set; }

        [Required(ErrorMessage = "Please provide your job seniority")]
        [DisplayName("Job Seniority")]
        [JsonProperty(PropertyName = "jobSeniority")]
        public string JobSeniority { get; set; }

        [DisplayName("Job Title (optional)")]
        [JsonProperty(PropertyName = "jobTitle")]
        public string JobTitle { get; set; }
    }
}
