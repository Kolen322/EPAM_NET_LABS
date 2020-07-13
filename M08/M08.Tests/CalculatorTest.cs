using System;
using NUnit.Framework;
using M08.PolishNotation;
namespace M08.Tests
{
    class CalculatorTest
    {
        [Test]
        [TestCase("5 1 2 + 4 * + 3 -", 14)]
        [TestCase("5 2 -", 3)]
        [TestCase("5 3 + 5 *", 40)]
        public void Calculate_NormalConditions_Test(string expression, double expectedResult)
        {
            Calculator testCalculator = new Calculator(expression);
            Assert.That(testCalculator.Calculate(), Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("5 1 2 + /")]
        [TestCase("5 1 +")]
        public void SetExpression_NormalConditions_Test(string expression)
        {
            Calculator testCalculator = new Calculator("5 2 +");
            testCalculator.SetExpression(expression);
            Assert.That(testCalculator.Expression, Is.EqualTo(expression));
        }

        [Test]
        [TestCase("5fgg")]
        [TestCase("fgg")]
        public void Constuctor_ArgumentException_Test(string expression)
        {
            ArgumentException expectedException = new ArgumentException("Expression contains letters");
            try
            {
                Calculator testCalculator = new Calculator(expression);
            }
            catch (Exception ex)
            {
                Assert.That(ex, Is.EqualTo(expectedException));
            }
        }

        [Test]
        [TestCase("5fgg")]
        [TestCase("fgg")]
        public void SetExpression_ArgumentException_Test(string expression)
        {
            ArgumentException expectedException = new ArgumentException("Expression contains letters");
            try
            {
                Calculator testCalculator = new Calculator("5 2 +");
                testCalculator.SetExpression(expression);
            }
            catch (Exception ex)
            {
                Assert.That(ex, Is.EqualTo(expectedException));
            }
        }

        [Test]
        [TestCase("5+6")]
        [TestCase("5 6 + 6/")]
        public void Calculate_ArgumentException_Test(string expression)
        {
            Calculator testCalculator = new Calculator(expression);
            Assert.Throws<ArgumentException>(() => testCalculator.Calculate());
        }

        [Test]
        [TestCase("5 6 + /")]
        [TestCase("5 6 + 3 * /")]
        public void Calculate_InvalidOperationException_Test(string expression)
        {
            Calculator testCalculator = new Calculator(expression);
            Assert.Throws<InvalidOperationException>(() => testCalculator.Calculate());
        }

        [Test]
        [TestCase("5 0 /")]
        public void Calculate_DivideByZeroException_Test(string expression)
        {
            Calculator testCalculator = new Calculator(expression);
            Assert.Throws<DivideByZeroException>(() => testCalculator.Calculate());
        }
    }
}
