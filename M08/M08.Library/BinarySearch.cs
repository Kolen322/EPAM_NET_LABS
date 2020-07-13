using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M08.Library
{
    /// <summary>
    /// Represents a Binary search
    /// </summary>
    public static class BinarySearch
    {
        /// <summary>
        /// Searches for an element in the sort collection
        /// </summary>
        /// <typeparam name="T">The type of elements</typeparam>
        /// <param name="data">Collection</param>
        /// <param name="value">Element which need to find</param>
        /// <returns>The index of element in collections or -1, if element don't contains in collection</returns>
        public static int Search<T>(T[] data, T value)
        {
            var left = 0;
            var right = data.GetUpperBound(0);
            if (left == right)
            {
                var result = data[left].Equals(value) ? left : -1;
                return result;
            }
            while (true)
            {
                if (right - left == 1)
                {
                    if (data[left].Equals(value))
                        return left;
                    if (data[right].Equals(value))
                        return right;
                    return -1;
                }
                else
                {
                    var middle = left + (right - left) / 2;
                    if (value.Equals(data[middle]))
                    {
                        return middle;
                    }
                    else if ((((IComparable)data[middle]).CompareTo((IComparable)value)) > 0)
                    {
                        right = middle;
                    }
                    else
                    {
                        left = middle;
                    }
                }
            }
        }
    }
}
