using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProjectEuler.Solutions.Problem4.ExtensionMethods;

namespace ProjectEuler.Solutions.Problem4
{
	[Export(typeof(IEulerProblemSolution))]
	public class Version1 : IEulerProblemSolution
	{
		private const long NUMBER_TO_FACTOR = 600851475143;
		public int ProblemNumber
		{
			get { return 4; }
		}

		public int ProblemVersion
		{
			get { return 1; }
		}

		public string Description
		{
			get
			{
				return "Find the largest palindrome made from the product of two 3-digit numbers.";
			}
		}

		private List<int> Palendromes = new List<int>();

		public string ComputeAnswer()
		{
			for (int i = 999; i > 0; i--)
			{
				for (int j = 999; j > 0; j--)
				{
					int product = j * i;
					if (IsPalendrome(product))
					{
						Palendromes.Add(product);
					}
				}
			}
			return Palendromes.Max().ToString();
		}

		private bool IsPalendrome(int number)
		{
			return number == number.Reverse();
		}
	}

	namespace ExtensionMethods
	{


		public static class Extensions
		{
			public static int Reverse(this int input)
			{
				const int modulus = 10;
				List<int> outputList = new List<int>();
				int currentDigit = 1;
				while (input!=0)
				{
					currentDigit *= modulus;
					int currentAmount = input%currentDigit;
					outputList.Add(currentAmount);
					input -= currentAmount*currentDigit/modulus;
				}
				outputList.Reverse();
				currentDigit = 1;
				int output = 0;
				foreach (int i in outputList)
				{
					output += i*currentDigit;
					currentDigit *= modulus;
				}
				return output;
			}
		}
	}
}