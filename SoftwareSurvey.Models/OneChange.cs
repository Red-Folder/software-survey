using Newtonsoft.Json;

namespace SoftwareSurvey.Models
{
    public class OneChange
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
