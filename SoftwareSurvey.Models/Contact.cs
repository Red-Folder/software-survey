using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareSurvey.Models
{
    public class Contact : StateObject
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

        public override bool IsValid() => IsEmailValid;

        [JsonIgnore]
        public bool IsEmailValid => !RequiresEmailAddress || !string.IsNullOrEmpty(Email);
        [JsonIgnore]
        public bool RequiresEmailAddress => SurveyResults || FollowUpQuestions || FurtherSurveys;
    }
}
