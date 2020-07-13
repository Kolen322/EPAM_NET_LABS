using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M08.Library;
using M08.Library.Collections;
namespace M08.PolishNotation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Calculator 5 1 2 + 4 * + 3 -");
            Calculator calculator = new Calculator("5 1 2 + 4 * + 3 -");
            Console.WriteLine($"Result: {calculator.Calculate()}");
            Console.ReadKey();
        }
    }
}
