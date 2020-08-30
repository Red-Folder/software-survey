namespace SoftwareSurvey.Models
{
    public class RequestDetails
    {
        public bool IsTest { get; set; }
        public string ConnectionIpAddress { get; set; }
        public string ForwardedIpAddress { get; set; }
    }
}
