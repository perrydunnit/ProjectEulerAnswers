using System;

namespace ProjectEuler.Solutions
{
    public class SampleSolution:IEulerProblemSolution
    {
        public int ProblemNumber
        {
            get { return int.MinValue; }
        }

        public string Description
        {
            get { return "This is not a real solution.  Please don't run it."; }
        }

        public string ComputeAnswer()
        {
            throw new NotImplementedException();
        }
    }
}
