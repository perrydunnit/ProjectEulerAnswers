using System;
using System.ComponentModel.Composition;

namespace ProjectEuler.Solutions.Problem3
{
	[Export(typeof(IEulerProblemSolution))]
	public class Version1 : IEulerProblemSolution
	{
		private const long NUMBER_TO_FACTOR = 600851475143;
		public int ProblemNumber
		{
			get { return 3; }
		}

		public int ProblemVersion
		{
			get { return 1; }
		}

		public string Description
		{
			get
			{
				return "What is the largest prime factor of the number 600851475143?";
			}
		}

		public string ComputeAnswer()
		{
			long largestPossible = GetLargestPossibleFactor(NUMBER_TO_FACTOR);
			for (long i = largestPossible; i >2; i-=2)
			{
				if (NUMBER_TO_FACTOR%i==0 && IsPrime(i))
				{
					return i.ToString();
				}
			}
			return "No prime factors were found.";

		}

		private long GetLargestPossibleFactor(long number)
		{
			long largestPossible = (long) Math.Sqrt(number);

			if (largestPossible%2==0)
			{
				largestPossible--;
			}
			return largestPossible;
		}

		private bool IsPrime(long number)
		{
			if (number<2)
			{
				return false;
			}
			if (number==2)
			{
				return true;
			}
			for (long i = GetLargestPossibleFactor(number); i > 2; i--)
			{
				if (number%i == 0)
				{
					return false;
				}
			}
			return true;

		}
	}
}