using Microsoft.Azure.Cosmos;
using SoftwareSurvey.Models;
using System.Net;
using System.Threading.Tasks;

namespace SoftwareSurvey.Services
{
    public class CosmosDbPersistanceManager : IPersistanceManager
    {
        private const string APPLICATION_NAME = "SoftwareSurvery";
        private const string DATABASE_NAME = "SoftwareSurvey";
        private const string CONTAINER_NAME = "Responses";

        private readonly PersistanceConfiguration _configuration;

        public CosmosDbPersistanceManager(PersistanceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Persist(SurveyResponse surveyResponse)
        {
            var client = new CosmosClient(_configuration.CosmosDbEndpoint,
                                          _configuration.CosmosDbPrimaryKey,
                                          new CosmosClientOptions() { ApplicationName = APPLICATION_NAME });

            Database database = await client.CreateDatabaseIfNotExistsAsync(DATABASE_NAME);
            Container container = await database.CreateContainerIfNotExistsAsync(CONTAINER_NAME, "/year");

            ItemResponse<SurveyResponse> response = await container.CreateItemAsync<SurveyResponse>(surveyResponse, new PartitionKey(surveyResponse.Year));

            return response.StatusCode == HttpStatusCode.Created;
        }
    }
}
