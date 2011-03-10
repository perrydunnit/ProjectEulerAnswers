namespace ProjectEuler.Solutions
{
    public interface IEulerProblemSolution
    {
        int ProblemNumber { get; }
        string Description { get; }
        string ComputeAnswer();
    }
}