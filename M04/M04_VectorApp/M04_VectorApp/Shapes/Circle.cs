using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_VectorApp.Shapes
{
    /// <summary>
    /// A class that describes the circle on a plane.
    /// </summary>
    class Circle : Shape
    {
        private double _radius;
        /// <summary>
        /// The radius of circle(must be greater than 0)
        /// </summary>
        public double Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Radius must be greater than 0");
                }
                else
                {
                    _radius = value;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the circle class that has a default point(0,0) and default radius(0)
        /// </summary>
        public Circle() : this(new Point(), 1)
        {
        }
        /// <summary>
        /// Initializes a new instance of the cricle class that has a specified point and radius
        /// </summary>
        /// <param name="startPoint">The point that has a start coordinates of circle</param>
        /// <param name="radius">The radius of circle</param>
        public Circle(Point startPoint, double radius) : base(startPoint)
        {
            Radius = radius;
        }
        /// <summary>
        /// Calculate the area of circle
        /// </summary>
        /// <returns>The area of circle</returns>
        public override double Area()
        {
            return Math.PI * _radius * _radius;
        }
        /// <summary>
        /// Print info of circle to console
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine($"Type of shape: Circle\nStart point: {StartPoint}\nRadius: {Radius}\nArea: {Area()}\nPerimeter: {Perimeter()}");
        }
        /// <summary>
        /// Calculate the perimeter of circle
        /// </summary>
        /// <returns>The perimeter of circle</returns>
        public override double Perimeter()
        {
            return 2 * Math.PI * _radius;
        }
    }
}
