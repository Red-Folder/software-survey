namespace SoftwareSurvey.Models
{
    public interface ISteps
    {
        bool HasNext(string currentPath);
        bool HasPrevious(string currentPath);
        string NextPath(string currentPath);
        string PreviousPath(string currentPath);

        string PathTitle(string currentPath);
        int StepNumber(string currentPath);
        int StepCount();
    }
}
