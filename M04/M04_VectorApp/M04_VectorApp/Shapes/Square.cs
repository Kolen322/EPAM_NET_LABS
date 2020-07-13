using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_VectorApp.Shapes
{
    /// <summary>
    /// A class that describes the square on a plane.
    /// </summary>
    class Square : Shape
    {
        private double _width;
        /// <summary>
        /// The width of square(must be greater than 0)
        /// </summary>
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width must be greater than 0");
                }
                else
                {
                    _width = value;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the square class that has a default point(0,0) and width(1)
        /// </summary>
        public Square() : this(new Point(0, 0), 1)
        {

        }
        /// <summary>
        /// Initializes a new instance of the square class that has a specified point and radius
        /// </summary>
        /// <param name="startPoint">The point that has a start coordinates of square</param>
        /// <param name="width">The width of square</param>
        public Square(Point startPoint, double width) : base(startPoint)
        {
            Width = width;
        }
        /// <summary>
        /// Calculate the area of square
        /// </summary>
        /// <returns>The area of square</returns>
        public override double Area()
        {
            return _width * _width;
        }
        /// <summary>
        /// Print info of square
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine($"Type of shape: Square\nStart point: {StartPoint}\nWidth: {Width}\nArea: {Area()}\nPerimeter: {Perimeter()}");
        }
        /// <summary>
        /// Calculate the perimeter of square
        /// </summary>
        /// <returns>The perimeter of square</returns>
        public override double Perimeter()
        {
            return _width * 4;
        }
    }
}
