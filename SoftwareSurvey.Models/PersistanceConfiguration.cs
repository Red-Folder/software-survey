namespace SoftwareSurvey.Models
{
    public class PersistanceConfiguration
    {
        public const string Section = "Persistance";

        public string CosmosDbEndpoint { get; set; }
        public string CosmosDbPrimaryKey { get; set; }
    }
}
