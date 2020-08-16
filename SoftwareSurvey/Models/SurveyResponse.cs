using Newtonsoft.Json;
using System;

namespace SoftwareSurvey.Models
{
    public class SurveyResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "createdTimestamp")]
        public DateTime CreatedTimestamp { get; set; }

        [JsonProperty(PropertyName = "demographic")]
        public Demographic Demographic { get; set; }
    }
}
