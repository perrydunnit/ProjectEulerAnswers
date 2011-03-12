using System.ComponentModel.Composition;

namespace ProjectEuler.Solutions.Problem1
{
	[Export(typeof(IEulerProblemSolution))]
    public class Version1 : IEulerProblemSolution
    {
        public int ProblemNumber
        {
            get { return 1; }
        }

        public int ProblemVersion
        {
            get { return 1; }
        }

        public string Description
        {
            get { return "Find the sum of all the multiples of 3 or 5 below 1000."; }
        }

        public string ComputeAnswer()
        {
            int total = 0;
            for (int i = 0; i < 100; i++)
            {
                if (i%5==0||i%3==0)
                {
                    total+=i;
                }
            }
            return total.ToString();
        }
    }
}