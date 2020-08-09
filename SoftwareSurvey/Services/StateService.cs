using SoftwareSurvey.Models;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareSurvey.Services
{
    public class StateService : IStateService
    {
        private readonly List<IStateObject> _stateObjects = new List<IStateObject>();

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
    }
}
