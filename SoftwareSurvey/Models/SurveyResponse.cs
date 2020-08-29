using Newtonsoft.Json;
using System;

namespace SoftwareSurvey.Models
{
    public class SurveyResponse
    {
        public SurveyResponse()
        {
            Id = Guid.NewGuid().ToString();
            Year = DateTime.Now.Year;
            CreatedTimestamp = DateTime.Now;
            HasBeenToStart = false;

            Demographic = new Demographic();
            SoftwareTypes = new SoftwareTypes();
            Experiences = new Experiences();
            OneChange = new OneChange();
            Contact = new Contact();
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "createdTimestamp")]
        public DateTime CreatedTimestamp { get; set; }

        [JsonIgnore]
        public bool HasBeenToStart { get; set; }

        [JsonProperty(PropertyName = "isTest")]
        public bool IsTest { get; set; }
        [JsonProperty(PropertyName = "connectionIpAddress")]
        public string ConnectionIpAddress { get; set; }
        [JsonProperty(PropertyName = "forwardedIpAddress")]
        public string ForwardedIpAddress { get; set; }

        [JsonProperty(PropertyName = "demographic")]
        public Demographic Demographic { get; set; }

        [JsonProperty(PropertyName = "softwareTypes")]
        public SoftwareTypes SoftwareTypes { get; set; }

        [JsonProperty(PropertyName = "experiences")]
        public Experiences Experiences { get; set; }

        [JsonProperty(PropertyName = "oneChange")]
        public OneChange OneChange { get; set; }

        [JsonProperty(PropertyName = "contact")]
        public Contact Contact { get; set; }
    }
}
