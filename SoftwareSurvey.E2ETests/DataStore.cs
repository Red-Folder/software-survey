using Microsoft.Azure.Cosmos;
using SoftwareSurvey.Models;
using System;
using System.Linq;

namespace SoftwareSurvey.E2ETests
{
    public class DataStore
    {
        private const string APPLICATION_NAME = "SoftwareSurvery";
        private const string DATABASE_NAME = "SoftwareSurvey";
        private const string CONTAINER_NAME = "Responses";

        private const string SETTING_COSMOSDB_ENDPOINT = "SoftwareSurvey:CosmosDbEndpoint";
        private const string SETTING_COSMOSDB_PRIMARY_KEY = "SoftwareSurvey:Persistance:CosmosDbPrimaryKey";

        private readonly string _cosmosDbEndpoint;
        private readonly string _cosmosDbPrimaryKey;

        public DataStore()
        {
            _cosmosDbEndpoint = Environment.GetEnvironmentVariable(SETTING_COSMOSDB_ENDPOINT);
            _cosmosDbPrimaryKey = Environment.GetEnvironmentVariable(SETTING_COSMOSDB_PRIMARY_KEY);

            if (string.IsNullOrEmpty(_cosmosDbEndpoint)) throw new ArgumentNullException($"{SETTING_COSMOSDB_ENDPOINT} must be set");
            if (string.IsNullOrEmpty(_cosmosDbPrimaryKey)) throw new ArgumentNullException($"{SETTING_COSMOSDB_PRIMARY_KEY} must be set");
        }

        public SurveyResponse Retrieve(string testRunId)
        {
            var client = new CosmosClient(_cosmosDbEndpoint,
                                            _cosmosDbPrimaryKey,
                                            new CosmosClientOptions() { ApplicationName = APPLICATION_NAME });

            Database database = client.GetDatabase(DATABASE_NAME);
            Container container = database.GetContainer(CONTAINER_NAME);

            var response = container.GetItemLinqQueryable<SurveyResponse>(true)
                                .Where(x => x.OneChange.Text == testRunId)
                                .AsEnumerable()
                                .FirstOrDefault();

            return response;
        }
    }
}
