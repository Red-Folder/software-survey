using SoftwareSurvey.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareSurvey.Services
{
    public class FakePersistanceManager : IPersistanceManager
    {
        public async Task<bool> Persist(SurveyResponse surveyResponse)
        {
            await Task.Run(() => Thread.Sleep(5000));
            return true;
        }
    }
}
