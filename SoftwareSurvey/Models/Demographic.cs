using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class Demographic: IStateObject
    {
        [Required(ErrorMessage = "Please provide company size")]
        [DisplayName("Company Size")]
        public string CompanySize { get; set; }

        [Required(ErrorMessage = "Please provide your job seniority")]
        [DisplayName("Job Seniority")]
        public string JobSeniority { get; set; }

        [DisplayName("Job Title (optional)")]
        public string JobTitle { get; set; }

        public bool IsTest => true;
    }
}
