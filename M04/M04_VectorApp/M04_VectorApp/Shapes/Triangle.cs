using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M04_VectorApp.Shapes
{
    /// <summary>
    /// A class that describes the triangle on a plane.
    /// </summary>
    class Triangle : Shape
    {
        private double _lengthOfFirstSide;
        private double _lengthOfSecondSide;
        private double _lengthOfThirdSide;
        private Point _thirdPoint;
        /// <summary>
        /// The second point of triangle
        /// </summary>
        public Point SecondPoint { get; private set; }
        /// <summary>
        /// The third point of triangle
        /// </summary>
        public Point ThirdPoint
        {
            get
            {
                return _thirdPoint;
            }
            private set
            {
                // A triangle does not exist if these three points lie on one straight line. Check it with oblique product.
                if (ObliqueProduct(StartPoint, SecondPoint) - ObliqueProduct(SecondPoint, value) == 0)
                {
                    throw new ArgumentException("Triangle doesn't exist with that coordinates");
                }
                else
                {
                    _thirdPoint = value;
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the triangle class that has a default point(0,0), point(1,1), point(2,1)
        /// </summary>
        public Triangle() : this(new Point(), new Point(1, 1), new Point(2, 1))
        {
            _lengthOfFirstSide = LengthLine(StartPoint, SecondPoint);
            _lengthOfSecondSide = LengthLine(StartPoint, _thirdPoint);
            _lengthOfThirdSide = LengthLine(SecondPoint, _thirdPoint);
        }
        /// <summary>
        /// Initializes a new instance of the triangle class that has a specified points.
        /// Points shouldn't be on one line.
        /// </summary>
        /// <param name="startPoint">The point that has a start coordinates of triangle</param>
        /// <param name="secondPoint">The second point of triangle</param>
        /// <param name="thirdPoint">The third point of triangle</param>
        public Triangle(Point startPoint, Point secondPoint, Point thirdPoint) : base(startPoint)
        {
            SecondPoint = secondPoint;
            ThirdPoint = thirdPoint;
            _lengthOfFirstSide = LengthLine(startPoint, secondPoint);
            _lengthOfSecondSide = LengthLine(startPoint, thirdPoint);
            _lengthOfThirdSide = LengthLine(secondPoint, thirdPoint);
        }
        // Oblique product of vectors
        private double ObliqueProduct(Point firstPoint, Point secondPoint)
        {
            return firstPoint.X * secondPoint.Y - secondPoint.X * firstPoint.Y;
        }
        // Calculate the length of side
        private double LengthLine(Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.Y, 2) + Math.Pow(secondPoint.Y - firstPoint.X, 2));
        }
        /// <summary>
        /// Calculate the area of triangle
        /// </summary>
        /// <returns>The area of triangle</returns>
        public override double Area()
        {
            double semiPerimeter = Perimeter() / 2;
            return Math.Sqrt(semiPerimeter * (semiPerimeter - _lengthOfFirstSide) * (semiPerimeter - _lengthOfSecondSide) * (semiPerimeter - _lengthOfThirdSide));
        }
        /// <summary>
        /// Print info of triangle
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine($"Type of shape: Triangle\nStart point: {StartPoint}\nSecond point: {SecondPoint}\nThird point: {ThirdPoint}\n" +
                $"Length of first side: {_lengthOfFirstSide}\nLength of second side: {_lengthOfSecondSide}\nLength of third side: {_lengthOfThirdSide}\nArea: {Area()}\nPerimeter: {Perimeter()}");
        }
        /// <summary>
        /// Calculate the perimeter of triangle
        /// </summary>
        /// <returns>The perimeter of triangle</returns>
        public override double Perimeter()
        {
            return _lengthOfFirstSide + _lengthOfSecondSide + _lengthOfThirdSide;
        }
    }
}
