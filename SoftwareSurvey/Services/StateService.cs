using SoftwareSurvey.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareSurvey.Services
{
    public class StateService : IStateService
    {
        private readonly List<IStateObject> _stateObjects = new List<IStateObject>();
        private readonly IPersistanceManager _persistanceManager;

        public StateService(IPersistanceManager persistanceManager)
        {
            _persistanceManager = persistanceManager;
        }

        public T GetOrNew<T>() where T : IStateObject, new()
        {
            var state = Get<T>();

            return state ?? new T();
        }

        public void Save<T>(T state) where T : IStateObject
        {
            var existingState = Get<T>();

            if (existingState != null)
            {
                _stateObjects.Remove(existingState);
            }

            _stateObjects.Add(state);
        }

        private T Get<T>() where T : IStateObject
        {
            return _stateObjects.OfType<T>().FirstOrDefault();
        }

        public async Task<bool> Persist()
        {
            try
            {
                return await _persistanceManager.Persist(_stateObjects);
            }
            catch { }

            return false;
        }
    }
}
