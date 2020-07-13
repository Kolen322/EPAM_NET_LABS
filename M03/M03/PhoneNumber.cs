using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M03
{
    /// <summary>
    /// Class that implements a method that takes a string which contains text with phone numbers.
    /// </summary>
    static class PhoneNumber
    {
        /// <summary>
        /// Method returns a list of strings with numbers from Text.txt and write them to Numbers.txt
        /// </summary>
        /// <param name="pathToReadFile">Path to read file</param>
        /// <returns>A list of strings with numbers from Text.txt</returns>
        public static List<string> GetPhoneNumbers(string pathToReadFile)
        {
            List<string> phoneNumbers = new List<string>();
            // Regex for numbers with format like
            // +X (XXX) XXX-XX-XX or X XXX XXX-XX-XX or +XXX (XX) XXX-XXXX
            Regex regex = new Regex(@"([+]+\d{3}\s+[(]+\d{2}[)]+\s+\d{3}[-]+\d{4}\W)*([+]*\d\s+[(]*\d{3}[)]*\s+\d{3}[-]+\d{2}[-]+\d{2}\W)*");
            MatchCollection matches;
            try
            {
                // Read lines in file to string "line".
                using (StreamReader streamReader = new StreamReader(pathToReadFile))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        matches = regex.Matches(line);
                        // If regex matches in line, add it into list of phone numbers
                        if (matches.Count > 0)
                        {
                            foreach (Match number in matches)
                            {
                                if (!String.IsNullOrEmpty(number.Value))
                                {
                                    phoneNumbers.Add(number.Value);
                                }
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return phoneNumbers;
        }
        /// <summary>
        /// Method writes list of phone numbers in new file
        /// </summary>
        /// <param name="phoneNumbers">List of phone numbers</param>
        /// <param name="pathToWriteFile">Path to file</param>
        public static void WritePhoneNumbersToNewFile(IEnumerable<string> phoneNumbers, string pathToWriteFile)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(pathToWriteFile, false, Encoding.Default))
                {
                    foreach (var number in phoneNumbers)
                    {
                        streamWriter.WriteLine(number);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
