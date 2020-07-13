using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M07
{
    /// <summary>
    /// Order type of sort
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Ascending
        /// </summary>
        Asc,
        /// <summary>
        /// Descending
        /// </summary>
        Desc
    }
    public enum ComparisonType
    {
        /// <summary>
        /// Sort the rows in order of sums of matrix row elements
        /// </summary>
        SumRows,
        /// <summary>
        /// Sort the rows in order of maximum element in a matrix row
        /// </summary>
        MaxElement,
        /// <summary>
        /// Sort the rows in order of minimum element in a matrix row
        /// </summary>
        MinElement
    }
}
