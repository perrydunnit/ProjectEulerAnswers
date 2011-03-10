using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Solutions.Problem2
{
    public class Version3 : IEulerProblemSolution
    {
        public int ProblemNumber
        {
            get { return 2; }
        }

        public int ProblemVersion
        {
            get { return 3; }
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
            int sum;
            int i = 1;
            for (; Fibonacci(i) < 4000000; i++)
            {
            }
            sum = fibs.FindAll(x => x % 2 == 0).Sum();
            return string.Format("Sum: {0}, max fib number: {1}", sum, i);
        }

        static readonly List<int> fibs = new List<int> { 1, 1 };

        private int Fibonacci(int j)
        {
            while (fibs.Count <= j)
            {
                int count = fibs.Count;
                fibs.Add(fibs[count - 1] + fibs[count - 2]);
            }

            return fibs[j];
        }
    }
}