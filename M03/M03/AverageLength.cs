using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M03
{
    /// <summary>
    /// Class that implements a method which allows
    /// to define an average word length in an input string.
    /// </summary>
    public static class AverageLength
    {
        /// <summary>
        /// Define an average word length in an input string.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Average word length.</returns>
        static public double GetWordAverage(string input)
        {
            if (!input.Equals(null))
            {
                if (input.Equals(string.Empty))
                {
                    return 0;
                }
                else
                {
                    char[] punctuations = { ',', '.', ':', ';', '?', '!', '-', '(', ')', '\"', ' ' };
                    string[] partsOfInputString = input.Split(punctuations);
                    int countOfWords = 0, sum = 0;
                    foreach (var part in partsOfInputString)
                    {
                        if (!String.IsNullOrEmpty(part))
                        {
                            sum += part.Length;
                            countOfWords++;
                        }
                    }
                    return (double)sum / countOfWords;
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
