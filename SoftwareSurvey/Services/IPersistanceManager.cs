using SoftwareSurvey.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareSurvey.Services
{
    public interface IPersistanceManager
    {
        Task<bool> Persist(List<IStateObject> stateObjects);
    }
}
