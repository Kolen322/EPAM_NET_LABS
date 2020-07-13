using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_VectorApp
{
    /// <summary>
    /// A class that describes a point on a plane.
    /// </summary>
    class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        /// <summary>
        /// Default constructor, point(0,0)
        /// </summary>
        public Point() : this(0, 0)
        { }
        /// <summary>
        /// Constructor of point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Override object method Equals.
        /// Determines whether two object instances are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Point p = (Point)obj;
                return (X == p.X) && (Y == p.Y);
            }
        }
        /// <returns>Boolean type</returns>
        /// <summary>
        /// Override object method GetHashCode.
        /// </summary>
        /// <returns>(Int32) A hast code for the current object.</returns>
        public override int GetHashCode()
        {
            // Use left-shift operator and XOR. It's necessary, when coordinates of objects equals(ex. (1,2) and (2,1)), with binary operators we avoid this problem.
            return (((int)X << 2) ^ (int)Y);
        }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format("X:{0}, Y:{1}", X, Y);
        }
    }
}
