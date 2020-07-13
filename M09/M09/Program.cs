using CommandLine;
using CommandLine.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace M09
{
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<TestOfStudent> collectionOfTests = null;
            using (JsonFileReader jsonFileReader = new JsonFileReader(new StreamReader(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Resources\students.json")))
            {
                collectionOfTests = jsonFileReader.ReadFromJson();
            }
            IEnumerable<TestOfStudent> filterCollection = FilterTestsOfStudent.Filter(collectionOfTests, args);
            foreach (var test in filterCollection)
            {
                Console.WriteLine(test);
            }
            Console.ReadKey();
        }

    }
}
