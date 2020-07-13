using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M08.PolishNotation
{
    /// <summary>
    /// Represents a calculator which evaluates expressions in Reverse Polish notation.
    /// </summary>
    public class Calculator
    {
        private string _expression;

        /// <summary>
        /// Expression in reverse polish notation
        /// </summary>
        public string Expression
        {
            get
            {
                return _expression;
            }
            private set
            {
                if (Regex.IsMatch(value, @"/\b[^\d\W]+\b/g"))
                {
                    throw new ArgumentException("Expression contains letters");
                }
                _expression = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the Calculator that has a expression
        /// </summary>
        /// <param name="expression">Expression in reverse polish notation</param>
        public Calculator(string expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// Calculate the expression and return the result
        /// </summary>
        /// <returns>Double result</returns>
        public double Calculate()
        {
            Stack<double> operandsOrOperations = new Stack<double>();
            string[] partOfExpression = Expression.Split(' ');
            foreach (var part in partOfExpression)
            {
                double number;
                bool isNum = double.TryParse(part, out number);
                if (isNum)
                {
                    operandsOrOperations.Push(number);
                }
                else
                {
                    switch (part)
                    {
                        case "+":
                            operandsOrOperations.Push(operandsOrOperations.Pop() + operandsOrOperations.Pop());
                            break;
                        case "*":
                            operandsOrOperations.Push(operandsOrOperations.Pop() * operandsOrOperations.Pop());
                            break;
                        case "-":
                            double subtrahend = operandsOrOperations.Pop();
                            operandsOrOperations.Push(operandsOrOperations.Pop() - subtrahend);
                            break;
                        case "/":
                            double divider = operandsOrOperations.Pop();
                            if (divider != 0.0)
                            {
                                operandsOrOperations.Push(operandsOrOperations.Pop() / divider);
                            }
                            else
                            {
                                throw new DivideByZeroException();
                            }
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            return operandsOrOperations.Pop();
        }

        /// <summary>
        /// Set the new value to expression
        /// </summary>
        /// <param name="expression">New expression</param>
        public void SetExpression(string expression)
        {
            Expression = expression;
        }
    }
}
