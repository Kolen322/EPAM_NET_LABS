using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_VectorApp.Shapes
{
    /// <summary>
    /// A class that describes the rectangle on a plane.
    /// </summary>
    class Rectangle : Shape
    {
        private double _width;
        private double _height;
        /// <summary>
        /// The width of rectangle(must be greater than 0)
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
        /// The height of rectangle(must be greater than 0)
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height must be greater than 0");
                }
                else
                {
                    _height = value;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the rectangle class that has a default point(0,0) and width(1), height(2)
        /// </summary>
        public Rectangle() : this(new Point(), 1, 2)
        {
        }
        /// <summary>
        /// Initializes a new instance of the rectangle class that has a specified point and width,height
        /// </summary>
        /// <param name="startPoint">The point that has a start coordinates of rectangle</param>
        /// <param name="width">The width of rectangle</param>
        /// <param name="height">The height of rectangle</param>
        public Rectangle(Point startPoint, double width, double height) : base(startPoint)
        {
            Width = width;
            Height = height;
        }
        /// <summary>
        /// Calculate the area of rectangle
        /// </summary>
        /// <returns>The area of rectangle</returns>
        public override double Area()
        {
            return _width * _height;
        }
        /// <summary>
        /// Print the info of rectangle
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine($"Type of shape: Rectangle\nStart point: {StartPoint}\nWidth: {Width}\nHeight: {Height}\nArea: {Area()}\nPerimeter: {Perimeter()}");
        }
        /// <summary>
        /// Calculate the perimeter of rectangle
        /// </summary>
        /// <returns>The perimeter of rectangle</returns>
        public override double Perimeter()
        {
            return _width * 2 + _height * 2;
        }
    }
}
