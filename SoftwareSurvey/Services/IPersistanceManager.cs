using SoftwareSurvey.Models;
using System.Threading.Tasks;

namespace SoftwareSurvey.Services
{
    public interface IPersistanceManager
    {
        Task<bool> Persist(SurveyResponse surveyResponse);
    }
}
