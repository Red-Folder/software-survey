using Microsoft.AspNetCore.Components.Server.Circuits;
using SoftwareSurvey.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareSurvey.Services
{
    public class IsConnectedCircuitHandler : CircuitHandler
    {
        private readonly SurveyResponse _surveyResponse;

        public IsConnectedCircuitHandler(SurveyResponse surveyResponse)
        {
            _surveyResponse = surveyResponse;
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _surveyResponse.SignalRCircuitEstablished = true;
            return Task.CompletedTask;
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _surveyResponse.SignalRCircuitEstablished = false;
            return Task.CompletedTask;
        }
    }
}
