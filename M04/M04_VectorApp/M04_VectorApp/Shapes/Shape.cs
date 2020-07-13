using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_VectorApp.Shapes
{
    /// <summary>
    /// A class that describes the shape on a plane.
    /// </summary>
    abstract class Shape
    {
        /// <summary>
        /// Starting point of shape
        /// </summary>
        public Point StartPoint { get; private set; }
        /// <summary>
        /// Initializes a new instance of the shape class that has a default point(0,0)
        /// </summary>
        public Shape() : this(new Point())
        {

        }
        /// <summary>
        /// Initializes a new instance of the shape class that has a specified start point
        /// </summary>
        /// <param name="startPoint">The point that has a start coordinates of shape</param>
        public Shape(Point startPoint)
        {
            StartPoint = startPoint;
        }
        /// <summary>
        /// Print info of shape to console
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Calculate the perimeter of shape
        /// </summary>
        /// <returns>The perimeter of shape</returns>
        public abstract double Perimeter();
        /// <summary>
        /// Calculate the area of shape
        /// </summary>
        /// <returns>The area of shape</returns>
        public abstract double Area();
    }
}
