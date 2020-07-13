using NUnit.Framework;
using M08.Library;
using System.Collections.Generic;
using System;
namespace M08.Tests
{
    class FibonacciTest
    {
        [Test]
        public void Realize_1Count_Test()
        {
            IEnumerable<int> expectedFibonacci = new int[] { 0 };
            IEnumerable<int> fibonacci = (IEnumerable<int>)Fibonacci.Realize(1);
            CollectionAssert.AreEqual(expectedFibonacci, fibonacci);
        }

        [Test]
        public void Realize_5Count_Test()
        {
            ICollection<int> expectedFibonacci = new int[] { 0, 1, 1, 2, 3 };
            IEnumerable<int> fibonacci = Fibonacci.Realize(5);
            CollectionAssert.AreEqual(expectedFibonacci, fibonacci);
        }

        [Test]
        public void Realize_ArgumentException_Test()
        {
            Exception expectedException = new ArgumentException();
            try
            {
                IEnumerable<int> fibonacci = Fibonacci.Realize(-5);
            }
            catch (ArgumentException ex)
            {
                Assert.That(ex, Is.EqualTo(expectedException));
            }
        }
    }
}
