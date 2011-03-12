using System;
using System.ComponentModel.Composition;

namespace ProjectEuler.Solutions.Problem4
{
    [Export(typeof(IEulerProblemSolution))]
    public class Version3 : IEulerProblemSolution
    {
        public int ProblemNumber
        {
            get { return 4; }
        }

        public int ProblemVersion
        {
            get { return 3; }
        }

        public string Description
        {
            get
            {
                return "Find the largest palindrome made from the product of two 3-digit numbers.";
            }
        }

        public string ComputeAnswer()
        {
            for (int i = 999; i > 0; i--)
            {
                for (int j = i; j > 0; j--)
                {
                    int product = j * i;
                    if (product > MaxPalendrome.Product && IsPalendrome(product))
                    {
                        MaxPalendrome= new ProductPalendrome(i, j);
                    }
                }
            }
            ProductPalendrome max = MaxPalendrome;
            return string.Format("{0} (product of {1} and {2})", max.Product, max.First, max.Second);
        }

        private ProductPalendrome _maxPalendrome = new ProductPalendrome(0, 0);
        private ProductPalendrome MaxPalendrome
        {
            get { return _maxPalendrome; }
            set { _maxPalendrome = value; }
        }

        private static bool IsPalendrome(int number)
        {
            return number == Reverse(number);
        }

        private static int Reverse(int input)
        {
            const int modulus = 10;
            int currentInput = input;
            int numOfDigits = (int)Math.Floor(Math.Log(input, modulus)) + 1;
            int result = 0;
            for (int i = 0; i < numOfDigits; i++)
            {
                result *= modulus;
                var power = (int)Math.Pow(modulus, (i + 1));
                result += currentInput % power / (power / modulus);
                currentInput = (int)(Math.Floor((decimal)(currentInput / power)) * power);
            }
            return result;
        }

    }
}