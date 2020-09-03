using Microsoft.ApplicationInsights;
using SoftwareSurvey.Models;
using System.Collections.Generic;

namespace SoftwareSurvey.Services
{
    public class EventLoggingService : IEventLoggingService
    {
        private const string ID = "SurveyResponseId";
        private const string IS_TEST = "IsTest";

        private readonly SurveyResponse _surveyResponse;
        private readonly TelemetryClient _telemetryClient;

        public EventLoggingService(SurveyResponse surveyResponse, TelemetryClient telemetryClient)
        {
            _surveyResponse = surveyResponse;
            _telemetryClient = telemetryClient;
        }

        public void TrackEvent(string eventName)
        {
            var properties = new Dictionary<string, string>();
            properties.Add(ID, _surveyResponse.Id);
            properties.Add(IS_TEST, _surveyResponse.IsTest.ToString());

            _telemetryClient.TrackEvent(eventName, properties);
        }
    }
}
