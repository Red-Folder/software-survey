namespace SoftwareSurvey.Models
{
    public class Step
    {
        public string Path { get; set; }
        public string Name { get; set; }

        public Step PreviousStep { get; set; }
        public Step NextStep { get; set; }
    }
}
