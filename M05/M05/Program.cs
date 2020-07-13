using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringToInt;
namespace M05
{
    class Program
    {
        static void Main(string[] args)
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Debug("Start application");
            Console.Write("Enter number to convert: ");
            try
            {
                string number = Console.ReadLine();
                int testConvert = IntConvert.Convert(number);
                Console.WriteLine($"Your number: {testConvert}");
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Your number is too large and doesn't fit in a type Int32");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
            logger.Debug("Stop application");
        }
    }
}
