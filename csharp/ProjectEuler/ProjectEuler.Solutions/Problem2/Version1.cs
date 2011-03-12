using System.ComponentModel.Composition;

namespace ProjectEuler.Solutions.Problem2
{
	[Export(typeof(IEulerProblemSolution))]
	public class Version1 : IEulerProblemSolution
	{
        public int ProblemNumber
        {
            get { return 2; }
        }

        public int ProblemVersion
        {
            get { return 1; }
        }

        public string Description
        {
            get
            {
                return
                    "By considering the terms in the Fibonacci sequence " + 
                    "whose values do not exceed four million, " +
                    "find the sum of the even-valued terms.";
            }
        }

        public string ComputeAnswer()
        {
            int sum = 0;
            int currentFibValue;
            int i = 1;
            for (; (currentFibValue=Fibonacci(i)) < 4000000; i++)
            {
                if (currentFibValue%2==0)
                {
                    sum += currentFibValue;
                }
            }
            return string.Format("Sum: {0}, max fib number: {1}", sum, i);
        }

        private int Fibonacci(int i)
        {
            if (i==1||i==2)
            {
                return 1;
            }
            return Fibonacci(i - 1) + Fibonacci(i - 2);
        }
    }
}