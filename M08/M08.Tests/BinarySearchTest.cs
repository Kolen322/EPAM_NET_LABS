using NUnit.Framework;
using M08.Library;
using System;

namespace M08.Tests
{
    class BinarySearchTest
    {
        [Test]
        [TestCase(-124, 0)]
        [TestCase(0, 2)]
        [TestCase(690, 8)]
        public void Search_Int_Test(int value, int expectedResult)
        {
            var result = BinarySearch.Search<int>(_testIntArray, value);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("A", 0)]
        [TestCase("Aa", 1)]
        [TestCase("D", 4)]
        public void Search_String_Test(string value, int expectedResult)
        {
            var result = BinarySearch.Search<string>(_testStringArray, value);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(null, 1)]
        [TestCase(null, null)]
        public void Search_NullReferenceException_Test(int[] array, int value)
        {
            Assert.Throws<NullReferenceException>(() => BinarySearch.Search<int>(array, value));
        }

        public int[] _testIntArray = new int[] { -124, -25, 0, 1, 5, 10, 25, 560, 690 };
        public string[] _testStringArray = new string[] { "A", "Aa", "B", "C", "D" };
    }
}
