using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using SoftwareSurvey.Models;
using System;
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
        private readonly ILogger _logger;

        public CosmosDbPersistanceManager(PersistanceConfiguration configuration, ILogger<CosmosDbPersistanceManager> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> Persist(SurveyResponse surveyResponse)
        {
            try
            {
                var client = new CosmosClient(_configuration.CosmosDbEndpoint,
                                              _configuration.CosmosDbPrimaryKey,
                                              new CosmosClientOptions() { ApplicationName = APPLICATION_NAME });

                Database database = await client.CreateDatabaseIfNotExistsAsync(DATABASE_NAME);
                Container container = await database.CreateContainerIfNotExistsAsync(CONTAINER_NAME, "/year");

                ItemResponse<SurveyResponse> response = await container.CreateItemAsync<SurveyResponse>(surveyResponse, new PartitionKey(surveyResponse.Year));

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                else
                {
                    _logger.LogError($"Unable to save record to CosmosDB - received {response.StatusCode} - {response.ActivityId}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save to CosmosDB");

                return false;
            }
        }
    }
}
