namespace ProjectEuler.Solutions
{
    public interface IEulerProblemSolution
    {
        int ProblemNumber { get; }
        int ProblemVersion { get; }
        string Description { get; }
        string ComputeAnswer();
    }
}