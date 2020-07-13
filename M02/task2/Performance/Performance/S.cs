using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance
{
    // Create a struct "S" with just one int field called "i".
    struct S : IComparable<S>
    {
        private int i;
        /// <summary>
        /// Constructor of S struct
        /// </summary>
        /// <param name="i">A integer number</param>
        public S(int i)
        {
            this.i = i;
        }
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes,
        /// follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(S other)
        {
            return i.CompareTo(other.i);
        }
    }
}
