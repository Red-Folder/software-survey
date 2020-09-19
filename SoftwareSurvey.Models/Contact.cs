using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class Contact : IValidatableObject
    {
        [DisplayName("With the results of the survery?")]
        [JsonProperty(PropertyName = "resultsOfTheSurvey")]
        public bool SurveyResults { get; set; }

        [DisplayName("For any follow up questions?")]
        [JsonProperty(PropertyName = "followUpQuestions")]
        public bool FollowUpQuestions { get; set; }

        [DisplayName("For future surveys?")]
        [JsonProperty(PropertyName = "furtherSuvey")]
        public bool FurtherSurveys { get; set; }

        [DisplayName("Email Address")]
        [EmailAddress]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return IsEmailValid ? null : new ValidationResult[] { new ValidationResult("Please provided email", new string[] { "Email" }) };
        }

        private bool IsEmailValid => !RequiresEmailAddress || !string.IsNullOrEmpty(Email);
        private bool RequiresEmailAddress => SurveyResults || FollowUpQuestions || FurtherSurveys;
    }
}
