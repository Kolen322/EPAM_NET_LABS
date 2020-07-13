using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_VectorApp.Shapes
{
    /// <summary>
    /// A class that describes the line on a plane.
    /// </summary>
    class Line : Shape
    {
        /// <summary>
        /// Finish point of the line
        /// </summary>
        public Point FinishPoint { get; private set; }
        /// <summary>
        /// Initializes a new instance of the line class that has a default start point(0,0), finish point(1,1).
        /// </summary>
        public Line() : this(new Point(), new Point(1, 1))
        {
        }
        /// <summary>
        /// Initializes a new instance of the line class that has a specified start point and finish point
        /// </summary>
        /// <param name="startPoint">The point that has a start coordinates of line</param>
        /// <param name="finishPoint">The point that has a finish coordinates of line</param>
        public Line(Point startPoint, Point finishPoint) : base(startPoint)
        {
            FinishPoint = finishPoint;
        }
        /// <summary>
        /// The line hasn't an area, method returns 0
        /// </summary>
        /// <returns>return 0</returns>
        public override double Area()
        {
            return 0;
        }

        private double LengthLine(Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.Y, 2) + Math.Pow(secondPoint.Y - firstPoint.X, 2));
        }
        /// <summary>
        /// Print info of line to console
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine($"Type of shape: Line\nStart point: {StartPoint}\nFinish point: {FinishPoint}\nLength: {Perimeter()}");
        }
        /// <summary>
        /// Calculate the perimeter of line
        /// </summary>
        /// <returns>The perimeter of line</returns>
        public override double Perimeter()
        {
            return LengthLine(StartPoint, FinishPoint);
        }
    }
}
