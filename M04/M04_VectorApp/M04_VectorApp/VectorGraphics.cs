using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M04_VectorApp.Shapes;

namespace M04_VectorApp
{
    /// <summary>
    /// A class that imitate a console application of vector graphics.
    /// </summary>
    class VectorGraphics
    {
        const int Line = 1;
        const int Circumference = 2;
        const int Rectangle = 3;
        const int Circle = 4;
        const int Triangle = 5;
        private Dictionary<string, Shape> _shapes { get; set; }
        /// <summary>
        /// Initializes a new instance of the vector graphics
        /// </summary>
        public VectorGraphics()
        {
            _shapes = new Dictionary<string, Shape>();
        }
        /// <summary>
        /// Start vector graphics app
        /// </summary>
        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("1. add shape\n2. print shape\n3. exit ");
                Console.Write("Enter: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddShape();
                        break;
                    case "2":
                        PrintShapes();
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void AddShape()
        {
            Console.Clear();
            Console.Write("Input name of shape: ");
            string nameOfShape = Console.ReadLine();
            Console.WriteLine("Select type of shape");
            Console.WriteLine("1. line\n2. circumference\n3. rectangle\n4. circle\n5. triangle");
            Console.Write("Enter: ");
            Int32.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case Line:
                    Shape line = AddLine();
                    if (line != null)
                        _shapes[nameOfShape] = line;
                    break;
                case Circumference:
                    Shape circumference = AddCircle();
                    if (circumference != null)
                        _shapes[nameOfShape] = circumference;
                    break;
                case Rectangle:
                    Shape rectangle = AddRectangle();
                    if (rectangle != null)
                        _shapes[nameOfShape] = rectangle;
                    break;
                case Circle:
                    Shape circle = AddCircle();
                    if (circle != null)
                        _shapes[nameOfShape] = circle;
                    break;
                case Triangle:
                    Shape triangle = AddTriangle();
                    if (triangle != null)
                        _shapes[nameOfShape] = triangle;
                    break;
                default: break;
            }
        }
        private Shape AddRectangle()
        {
            Console.Clear();
            try
            {
                Console.Write("Enter coordinate x of start position: ");
                double xOfStartPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate y of start position: ");
                double yOfStartPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter width: ");
                double width = Double.Parse(Console.ReadLine());
                Console.Write("Enter height: ");
                double height = Double.Parse(Console.ReadLine());
                Shape rectangle = new Rectangle(new Point(xOfStartPosition, yOfStartPosition), width, height);
                return rectangle;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        private Shape AddCircle()
        {
            Console.Clear();
            try
            {
                Console.Write("Enter coordinate x of start position: ");
                double xOfStartPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate y of start position: ");
                double yOfStartPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter radius: ");
                double radius = Double.Parse(Console.ReadLine());
                Shape circle = new Circle(new Point(xOfStartPosition, yOfStartPosition), radius);
                return circle;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        private Shape AddTriangle()
        {
            Console.Clear();
            try
            {
                Console.Write("Enter coordinate x of start position: ");
                double xOfStartPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate y of start position: ");
                double yOfStartPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate x of second point: ");
                double xOfSecondPoint = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate y of second point: ");
                double yOfSecondPoint = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate x of third point: ");
                double xOfThirdPoint = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate y of third point: ");
                double yOfThirdPoint = Double.Parse(Console.ReadLine());
                Shape triangle = new Triangle(new Point(xOfStartPosition, yOfStartPosition), new Point(xOfSecondPoint, yOfSecondPoint), new Point(xOfThirdPoint, yOfThirdPoint));
                return triangle;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        private Shape AddLine()
        {
            Console.Clear();
            try
            {
                Console.Write("Enter coordinate x of start position: ");
                double xOfStartPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate y of start position: ");
                double yOfStartPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate x of finish position: ");
                double xOfFinishPosition = Double.Parse(Console.ReadLine());
                Console.Write("Enter coordinate y of finish position: ");
                double yOfFinishPosition = Double.Parse(Console.ReadLine());
                Shape line = new Line(new Point(xOfStartPosition, yOfStartPosition), new Point(xOfFinishPosition, yOfFinishPosition));
                return line;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        private void PrintShapes()
        {
            Console.Clear();
            Console.WriteLine("Your's shapes");
            foreach (var shape in _shapes)
            {
                Console.WriteLine($"{shape.Key} --- {shape.Value.GetType().Name}");
            }
            Console.Write("Enter name: ");
            string nameOfShape = Console.ReadLine();
            Console.Clear();
            _shapes[nameOfShape].Draw();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
