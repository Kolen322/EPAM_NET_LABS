using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M03
{
    /// <summary>
    /// Class that implements a method which reverses all of the words within the string passed in.
    /// </summary>
    public static class ReverseWords
    {
        /// <summary>
        /// Method which reverses all of the words within the string passed in.
        /// </summary>
        /// <param name="toReverse">Input string</param>
        public static void Reverse(ref string toReverse)
        {
            if (!toReverse.Equals(null))
            {
                string[] partsOfInputString = toReverse.Split(' ');
                var countOfParts = partsOfInputString.Length - 1;
                // Swap strings in "parts".
                for (int i = 0; i <= countOfParts / 2; i++)
                {
                    Swap(ref partsOfInputString[i], ref partsOfInputString[countOfParts - i]);
                }
                StringBuilder reverseString = new StringBuilder();
                // Create StringBuilder with reverse parts.
                foreach (var part in partsOfInputString)
                {
                    reverseString.Append(part + " ");
                }
                // Remove last whitespace
                reverseString.Remove(reverseString.Length - 1, 1);
                toReverse = reverseString.ToString();
            }
            else
            {
                throw new NullReferenceException();
            }

        }
        // Method which swap two strings
        private static void Swap(ref string one, ref string two)
        {
            string temp = one;
            one = two;
            two = temp;
        }
    }
}
