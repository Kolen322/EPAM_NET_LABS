using NUnit.Framework;
using System;
namespace M03.Tests
{
    class DoublesCharactersTests
    {
        [Test]
        [TestCase("Hello test","t e","Heello tteestt")]
        [TestCase("Hello test", "t, , , , e","Heello tteestt")]
        [TestCase("Hello test", _empty, "Hello test")]
        [TestCase(_empty, "fgg", _empty)]
        public void Doubles_NormalConditions_Test(string input, string characters, string expectedResult)
        {
            DoublesCharacters.Doubles(ref input, characters);
            Assert.That(input, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("Hello",null)]
        [TestCase(null,"Fgg")]
        public void Doubles_NullReferenceException_Test(string input, string characters)
        {
            Assert.Throws<NullReferenceException>(() => DoublesCharacters.Doubles(ref input, characters));
        }

        private const string _empty = "";
    }
}
