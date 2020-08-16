using Newtonsoft.Json;
using System;

namespace SoftwareSurvey.Models
{
    public class SurveyResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public int Year { get; set; }

        public DateTime CreatedTimestamp { get; set; }

        public Demographic Demographic { get; set; }
    }
}
