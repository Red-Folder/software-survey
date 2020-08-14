using SoftwareSurvey.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoftwareSurvey.Services
{
    public class PersistanceManager : IPersistanceManager
    {
        public async Task<bool> Persist(List<IStateObject> stateObjects)
        {
            await Task.Run(() => Thread.Sleep(5000));
            return true;
        }
    }
}
