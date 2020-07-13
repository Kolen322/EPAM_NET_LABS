using System;
using NUnit.Framework;

namespace M09.Tests
{
    class TestOfStudentTests
    {
        [Test]
        public void ToString_Test()
        {
            Assert.That(_testStudent.ToString(), Is.EqualTo("Ivan Math 12.12.1998 5"));
        }

        [Test]
        public void Equals_Test()
        {
            TestOfStudent secondStudent = new TestOfStudent("Ivan", "Math", new DateTime(1998, 12, 12), 5);
            Assert.That(_testStudent.Equals(secondStudent), Is.EqualTo(true));
        }

        public void GetHashCode_Test()
        {
            Assert.That(_testStudent.GetHashCode(), Is.EqualTo(5 + _testStudent.Date.GetHashCode()));
        }

        [Test]
        public void ArgumentException_Test()
        {
            Assert.Throws<ArgumentException>(() => new TestOfStudent("Ivan", "Russian", new DateTime(1998, 12, 12), -5));
        }

        private static TestOfStudent _testStudent = new TestOfStudent("Ivan", "Math", new DateTime(1998, 12, 12), 5);
    }
}
