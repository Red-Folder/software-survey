using Newtonsoft.Json;

namespace SoftwareSurvey.Models
{
    public class OneChange : StateObject
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
