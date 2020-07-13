using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M08.Library
{
    /// <summary>
    /// Represents a Fibonacci's numbers
    /// </summary>
    public static class Fibonacci
    {
        /// <summary>
        /// Calculate the fibonacci's numbers
        /// </summary>
        /// <param name="count">Count of numbers</param>
        /// <returns>Returns the IEnumerable collections with fibonacci's numbers</returns>
        public static IEnumerable<int> Realize(int count)
        {
            if (count > 0)
            {
                int prev = -1;
                int next = 1;
                int sum = 0;
                for (int i = 0; i < count; i++)
                {
                    sum = prev + next;
                    prev = next;
                    next = sum;
                    yield return sum;
                }
            }
            else
            {
                throw new ArgumentException("Count should be greater than zero");
            }

        }
    }
}
