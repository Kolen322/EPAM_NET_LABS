using NUnit.Framework;
using M03;
using System;

namespace M03.Tests
{
    class BigNumberTests
    {
        [Test]
        [TestCase("0", "0", "0")]
        [TestCase("999999", "11111111", "12111110")]
        [TestCase("9999999999", "999", "10000000998")]
        [TestCase("198", "222", "420")]
        [TestCase("999", "9999999999", "10000000998")]
        public void Sum_NormalConditions_Test(string firstNumber, string secondNumber, string expectedResult)
        {
            var result = BigNumber.Sum(firstNumber, secondNumber);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("-4242", "2442")]
        [TestCase("52525", "-5225")]
        [TestCase("fdfsa", "2442")]
        [TestCase("242442", "sffs")]
        public void Sum_FormatException_Test(string firstNumber, string secondNumber)
        {
            string result = null;
            Assert.Throws<FormatException>(() => result = BigNumber.Sum(firstNumber, secondNumber));
        }

        [Test]
        [TestCase(null,"2444")]
        [TestCase("4455",null)]
        [TestCase("55",_empty)]
        [TestCase(_empty,"566")]
        public void Sum_NullReferenceException_Test(string firstNumber, string secondNumber)
        {
            string result = null;
            Assert.Throws<NullReferenceException>(() => result = BigNumber.Sum(firstNumber, secondNumber));
        }

        private const string _empty = "";
    }
}
