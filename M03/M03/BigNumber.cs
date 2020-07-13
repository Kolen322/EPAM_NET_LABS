using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M03
{
    /// <summary>
    /// Class that implements a method, that returns the sum of two big numbers (bigger than long).
    /// </summary>
    public static class BigNumber
    {
        /// <summary>
        /// Adds two big numbers and returns the result in string
        /// Function works with only positive numbers
        /// </summary>
        /// <param name="firstNumber">First big number</param>
        /// <param name="secondNumber">Second big number</param>
        /// <returns>The sum of two big numbers</returns>
        static public string Sum(string firstNumber, string secondNumber)
        {
            if (!string.IsNullOrEmpty(firstNumber) && !string.IsNullOrEmpty(secondNumber))
            {
                if (!firstNumber.Any(numeral => char.IsLetter(numeral) || char.IsPunctuation(numeral)) &&
                    !secondNumber.Any(numeral => char.IsLetter(numeral) || char.IsPunctuation(numeral)))
                {
                    Stack<char> firstN = new Stack<char>(firstNumber.ToCharArray());
                    Stack<char> secondN = new Stack<char>(secondNumber.ToCharArray());
                    // Look at which number is greater and start summ digits
                    string result = firstN.Count > secondN.Count ? RealizeSum(firstN, secondN) : RealizeSum(secondN, firstN);
                    return result;
                }
                else
                {
                    throw new FormatException("Input strings contain letters");
                }
            }
            else
            {
                throw new NullReferenceException("Input string doesn't contains value");
            }
        }

        static private string RealizeSum(Stack<char> firstNumber, Stack<char> secondNumber)
        {
            // In ASCII table, numbers start from 48
            const int NumberASCII = 48;
            StringBuilder result = new StringBuilder();
            // Most Significant Byte
            int MSB = 0;
            int sum = 0;
            while (firstNumber.Any())
            {
                sum = secondNumber.Any()
                      ? (int)Char.GetNumericValue(firstNumber.Pop()) + (int)Char.GetNumericValue(secondNumber.Pop()) + MSB
                      : (int)Char.GetNumericValue(firstNumber.Pop()) + MSB;
                MSB = 0;
                if (sum > 9)
                {
                    MSB++;
                    result.Insert(0, Convert.ToChar(sum % 10 + NumberASCII));
                }
                else
                {
                    result.Insert(0, Convert.ToChar(sum + NumberASCII));
                }
            }
            // If msb isn't 0, add new number in result(Ex. 9999 + 99 need to remember "1" to the end of number and create new digit)
            if (MSB != 0)
            {
                result.Insert(0, 1);
            }
            return result.ToString();
        }
    }
}
