using System.Collections.Generic;
using System.Linq;

namespace SoftwareSurvey.Models
{
    public class Steps: ISteps
    {
        private readonly List<Step> _steps;

        public Steps(List<Step> steps)
        {
            _steps = steps;
        }

        public string CurrentPathTitle(string currentPath) => FromCurrentPath(currentPath)?.PageTitle ?? "Unknown";

        public bool HasNext(string currentPath) =>  NextPath(currentPath) != null;

        public bool HasPrevious(string currentPath) => PreviousPath(currentPath) != null;

        public string NextPath(string currentPath) => FromCurrentPath(currentPath)?.NextStep?.Path;

        public string PreviousPath(string currentPath) => FromCurrentPath(currentPath)?.PreviousStep?.Path;

        private Step FromCurrentPath(string currentPath) => _steps.FirstOrDefault(x => x.Path == currentPath);
    }
}
