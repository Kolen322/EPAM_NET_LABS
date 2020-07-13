using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_VectorApp.Shapes
{
    /// <summary>
    /// A class that describes the circumference on a plane.
    /// </summary>
    class Circumference : Circle
    {
        /// <summary>
        /// Initializes a new instance of the circumference class that has a default point(0,0) and default radius(0)
        /// </summary>
        public Circumference()
        {
        }
        /// <summary>
        /// Initializes a new instance of the circumference class that has a specified point and radius
        /// </summary>
        /// <param name="startPoint">The point that has a start coordinates of circumference</param>
        /// <param name="radius">The radius of circumference</param>
        public Circumference(Point startPoint, int radius) : base(startPoint, radius)
        {

        }
        /// <summary>
        /// The circumference hasn't an area, method returns 0.
        /// </summary>
        /// <returns>return 0</returns>
        public override double Area()
        {
            return 0;
        }
        /// <summary>
        /// Print info of circumference to console
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine($"Type of shape: Circumference\nStart point: {StartPoint}\nRadius: {Radius}\nLength: {Perimeter()}");
        }
    }
}
