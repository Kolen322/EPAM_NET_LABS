using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_Game
{
    /// <summary>
    /// A class that describes a point on a plane.
    /// </summary>
    class Point
    {
        private int _x { get; set; }
        private int _y { get; set; }
        /// <summary>
        /// Constructor of point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
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
                return (_x == p._x) && (_y == p._y);
            }
        }
        /// <summary>
        /// Return value of X
        /// </summary>
        /// <returns>Value of X</returns>
        public int GetX()
        {
            return _x;
        }
        /// <summary>
        /// Return value of Y
        /// </summary>
        /// <returns>Value of Y</returns>
        public int GetY()
        {
            return _y;
        }
        /// <summary>
        /// Increment value of X
        /// </summary>
        public void IncrementX()
        {
            _x++;
        }
        /// <summary>
        /// Increment value of Y
        /// </summary>
        public void IncrementY()
        {
            _y++;
        }
        /// <summary>
        /// Decrement value of X
        /// </summary>
        public void DecrementX()
        {
            _x--;
        }
        /// <summary>
        /// Decrement value of Y
        /// </summary>
        public void DecrementY()
        {
            _y--;
        }
        /// <summary>
        /// Adds to X the entered value
        /// </summary>
        /// <param name="value"> Value which need add to X</param>
        public void AddX(int value)
        {
            _x += value;
        }
        /// <summary>
        /// Subtract from X the entered value
        /// </summary>
        /// <param name="value"> Value which need subtract from X</param>
        public void SubX(int value)
        {
            _x -= value;
        }
        /// <summary>
        /// Adds to Y the entered value
        /// </summary>
        /// <param name="value"> Value which need add to Y</param>
        public void AddY(int value)
        {
            _y += value;
        }
        /// <summary>
        /// Subtract from Y the entered value
        /// </summary>
        /// <param name="value"> Value which need subtract from Y</param>
        public void SubY(int value)
        {
            _y -= value;
        }
        /// <summary>
        /// Set new value to X
        /// </summary>
        /// <param name="value"> Value which need to set</param>
        public void SetX(int value)
        {
            _x = value;
        }
        /// <summary>
        /// Set new value to Y
        /// </summary>
        /// <param name="value"> Value which need to set</param>
        public void SetY(int value)
        {
            _y = value;
        }
        /// <returns>Boolean type</returns>
        /// <summary>
        /// Override object method GetHashCode.
        /// </summary>
        /// <returns>(Int32) A hast code for the current object.</returns>
        public override int GetHashCode()
        {
            // Use left-shift operator and XOR. It's necessary, when coordinates of objects equals(ex. (1,2) and (2,1)), with binary operators we avoid this problem.
            return (_x << 2) ^ _y;
        }
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format("X:{0}, Y:{1}", _x, _y);
        }
    }
}
