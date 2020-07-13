using NUnit.Framework;
using System;

namespace M03.Tests
{
    class ReverseWordsTests
    {
        [Test]
        [TestCase("Test reverse","reverse Test")]
        [TestCase("Test reverse odd", "odd reverse Test")]
        [TestCase("Test","Test")]
        [TestCase(_empty,_empty)]
        public void Reverse_NormalConditions_Test(string input, string expectedResult)
        {
            ReverseWords.Reverse(ref input);
            Assert.That(input, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase(null)]
        public void Reverse_NullReferenceException_Test(string input)
        {
            Assert.Throws<NullReferenceException>(() => ReverseWords.Reverse(ref input));
        }

        private const string _empty = "";
    }
}
