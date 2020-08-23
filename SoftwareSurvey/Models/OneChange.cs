using Newtonsoft.Json;

namespace SoftwareSurvey.Models
{
    public class OneChange : IStateObject
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
