using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            C[] classes = { };
            S[] structs = { };
            CalculateMemory(ref classes, ref structs);
            CalculateTimeOfSort(classes, structs);
            Console.ReadKey();
        }
        // Calculate PrivateMemorySize64 delta before and after arrays initialization (for each array separately).
        static public void CalculateMemory(ref C[] classes, ref S[] structs)
        {
            Random rand = new Random();
            // Calculate PrivateMemorySize64 before arrays initialization.
            long memory = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"PrivateMemory64 before arrays initialization:{memory}");
            // Create an array of 100000 "C" called "classes" and initialize it with random numbers.
            classes = Enumerable.
                Repeat(0, 100000).
                Select(i => new C(rand.Next(Int32.MinValue, Int32.MaxValue))).
                ToArray();
            // Calculate PrivateMemorySize64 after C array initialization.
            long cMemory = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"PrivateMemory64 after C array initialization:{cMemory}");
            // Create an array of 100000 "S" called "structs" and initialize it with random numbers.
            structs = Enumerable.
                Repeat(0, 100000).
                Select(i => new S(rand.Next(Int32.MinValue, Int32.MaxValue))).
                ToArray();
            // Calculatte PrivateMemorySize64 after S array initialization.
            long sMemory = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"PrivateMemory64 after S array initialization:{sMemory}");
            // Print the difference between C and S deltas.
            Console.WriteLine($"The difference between C and S deltas:{(cMemory - memory) - (sMemory - cMemory)}");
        }
        // Execute Array.Sort<C>(classes) and Array.Sort<S>(structs) and print execution time to the console.
        static public void CalculateTimeOfSort(C[] cArray, S[] sArray)
        {
            // 1 sec = 1000 milisecond. For convient print divide msc in 10.
            const int FormatMiliseconds = 10;
            // Initialize StopWatch.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // Sort C array.
            Array.Sort<C>(cArray);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            // Using String.Format for convenient print.
            string elapsedTimeCArray = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / FormatMiliseconds);
            // Print time of Sort C array.
            Console.WriteLine($"Time of sort C array:" + elapsedTimeCArray);
            // Reset StopWatch.
            stopWatch.Reset();
            // Same for S array.
            stopWatch.Start();
            Array.Sort<S>(sArray);
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            string elapsedTimeSArray = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / FormatMiliseconds);
            // Print time of Sort S array.
            Console.WriteLine($"Time of sort S array:" + elapsedTimeSArray);
        }
    }
}
