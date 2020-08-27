using System.Collections.ObjectModel;

namespace SoftwareSurvey.Models
{
    public interface ISteps
    {
        public bool HasNext(string currentPath);
        public bool HasPrevious(string currentPath);
        public string NextPath(string currentPath);
        public string PreviousPath(string currentPath);

        public string PathTitle(string currentPath);
        int StepNumber(string currentPath);
        int StepCount();
    }
}
