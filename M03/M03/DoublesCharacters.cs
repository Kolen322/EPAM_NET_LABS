using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M03
{
    /// <summary>
    /// Class that implements a method that doubles in the
    /// first string parameter all characters belonging the second string parameter.
    /// </summary>
    public static class DoublesCharacters
    {
        /// <summary>
        /// Method that doubles in the first string paramer all characters belonging the second string parameter.
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="characters">Characters</param>
        static public void Doubles(ref string input, string characters)
        {
            if (!input.Equals(null))
            {
                HashSet<char> uniqueCh = new HashSet<char>();
                char[] separators = { ',', '.', ':', ';', '?', '!', '-', '(', ')', '\"', ' ' };
                string[] partsOfCharactersString = characters.Split(separators);
                // Add unique characters in HashSet
                foreach (var part in partsOfCharactersString)
                {
                    foreach (var character in part.ToCharArray())
                    {
                        uniqueCh.Add(character);
                    }
                }
                StringBuilder doubledInputSb = new StringBuilder(input);
                // If character in StringBuilder contains in HashSet, double it in first string.
                for (int i = 0; i < doubledInputSb.Length; i++)
                {
                    if (uniqueCh.Contains(doubledInputSb[i]))
                    {
                        doubledInputSb.Insert(i, doubledInputSb[i]);
                        i++;
                    }
                }
                input = doubledInputSb.ToString();
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
