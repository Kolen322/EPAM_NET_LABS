using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M03
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test AWordLength class.
            string testAverageWordLength = "AAA AAA AAA AAAAAA AAAAAA";
            Console.WriteLine($"Average word length of {testAverageWordLength} is {AverageLength.GetWordAverage(testAverageWordLength)}");

            // Test DoublesCharacters class.
            string characters = "abs bas z,,,,z";
            string testDoublesCharacters = "abz asrqws zaafs b";
            Console.WriteLine($"String before doubles: {testDoublesCharacters}");
            DoublesCharacters.Doubles(ref testDoublesCharacters, characters);
            Console.WriteLine($"String after doubles: {testDoublesCharacters}");

            // Test BigNumber class.
            string firstNumber = "9999999999999999999999999";
            string secondNumber = "999999999999999999";
            Console.WriteLine($"Test BigNumber:\n{firstNumber} + {secondNumber} = {BigNumber.Sum(firstNumber, secondNumber)}");

            // Test ReverseWords class.
            string testReverse = "The greatest victory is that which requires no battle";
            Console.WriteLine($"String before reverse: {testReverse}");
            ReverseWords.Reverse(ref testReverse);
            Console.WriteLine($"String after reverse: {testReverse}");

            // Test PhoneNumber class.
            List<string> testPhoneNumbers = new List<string>();
            testPhoneNumbers = PhoneNumber.GetPhoneNumbers("Text.txt");
            PhoneNumber.WritePhoneNumbersToNewFile(testPhoneNumbers, "Numbers.txt");
            foreach (var number in testPhoneNumbers)
            {
                Console.WriteLine(number);
            }
            Console.ReadKey();
        }
    }
}
