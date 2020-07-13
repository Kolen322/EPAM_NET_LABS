using NUnit.Framework;
using System;

namespace M03.Tests
{
    class AverageLengthTests
    {
        [Test]
        [TestCase(_empty,0)]
        [TestCase("Aaa aaa",3)]
        [TestCase("Ht ht",2)]
        [TestCase("Tffg       fg",3)]
        [TestCase("fsf, ghf, tft",3)]
        [TestCase("fgggg rf fg f", 2.5)]
        public void GetWordAverage_NormalConditions_Test(string input, double expectedResult)
        {
            var result = AverageLength.GetWordAverage(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(null)]
        public void GetWordAverage_NullException_Test(string input)
        {
            Assert.Throws<NullReferenceException>(() => AverageLength.GetWordAverage(input));
        }

        private const string _empty = "";
    }
}
