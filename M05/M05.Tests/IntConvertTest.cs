using NUnit.Framework;
using System;
using StringToInt;
namespace Tests
{
    public class IntConvertTest
    {
        [Test]
        [TestCase("101", 101)]
        [TestCase("-1567", -1567)]
        [TestCase(_intMaxValue, Int32.MaxValue)]
        [TestCase(_intMinValue, Int32.MinValue)]
        [TestCase("0", 0)]
        public void Convert_StringToInt_Test(string number, int expectedResult)
        {
            var result = IntConvert.Convert(number);
            Assert.That(result, Is.EqualTo(expectedResult));

        }

        [Test]
        [TestCase("--5677")]
        [TestCase("45fgfg55g")]
        public void Convert_FormatException_Test(string number)
        {
            Assert.Throws<FormatException>(() => IntConvert.Convert(number));
        }

        [Test]
        [TestCase(_overFlowMax)]
        [TestCase(_overFlowMin)]
        public void Convert_OverflowException_Test(string number)
        {
            Assert.Throws<OverflowException>(() => IntConvert.Convert(number));
        }

        private const string _overFlowMax = "2147843649";
        private const string _overFlowMin = "-2147843649";
        private const string _intMaxValue = "2147483647";
        private const string _intMinValue = "-2147483648";
    }
}