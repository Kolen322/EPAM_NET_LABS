using NUnit.Framework;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Moq;

namespace M09.Tests
{
    class FilterTestsOfStudentTests
    {
        [Test]
        public void Filter_AfterNameIvanFlag_Test()
        {
            var args = new string[] { "--name", "Ivan" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterNameIvanFlag);
        }

        [Test]
        public void Filter_AfterMinMarkThreeFlag_Test()
        {
            var args = new string[] { "--minmark", "3" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterMinMarkThreeFlag);
        }

        [Test]
        public void Filter_AfterMaxMarkThreeFlag_Test()
        {
            var args = new string[] { "--maxmark", "3" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterMaxMarkThreeFlag);
        }

        [Test]
        public void Filter_AfterDateFromFlag_Test()
        {
            var args = new string[] { "--datefrom", "01/01/2019" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterDateFromFlag);
        }

        [Test]
        public void Filter_AfterDateToFlag_Test()
        {
            var args = new string[] { "--dateto", "01/01/2019" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterDateToFlag);
        }

        [Test]
        public void Filter_AfterTestMathFlagFlag_Test()
        {
            var args = new string[] { "--test", "Math" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterTestMathFlag);
        }

        [Test]
        public void Filter_AfterSortAscByMark_Test()
        {
            var args = new string[] { "--sort", "mark", "asc" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterSortAscByMark);
        }

        [Test]
        public void Filter_AfterSortDescByMark_Test()
        {
            var args = new string[] { "--sort", "mark", "desc" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterSortDescByMark);
        }

        [Test]
        public void Filter_AfterSortAscByDate_Test()
        {
            var args = new string[] { "--sort", "date", "asc" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterSortAscByDate);
        }

        [Test]
        public void Filter_AfterSortDescByDate_Test()
        {
            var args = new string[] { "--sort", "date", "desc" };
            var result = FilterTestsOfStudent.Filter(_testArray, args);
            CollectionAssert.AreEqual(result, _expectedArrayAfterSortDescByDate);
        }

        [Test]
        public void Filter_ArgumentNullException_Test()
        {
            var args = new string[] { "--minmark", "3" };
            Assert.Throws<ArgumentNullException>(() => FilterTestsOfStudent.Filter(null, args));
        }

        [Test]
        public void Filter_ArgumentException_Test()
        {
            var args = new string[] { "--sort", "fffff", "sfsfsfs" };
            Assert.Throws<ArgumentException>(() => FilterTestsOfStudent.Filter(_testArray, args));
        }

        private static List<TestOfStudent> _testArray = new List<TestOfStudent>
            {
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5),
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4),
                new TestOfStudent("Olga", "English", new DateTime(2015,12,11),2),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3),
                new TestOfStudent("Oleg", "Russian", new DateTime(2019,12,13),4),
                new TestOfStudent("Oleg", "English", new DateTime(2019,12,20),5)
            };
        private static List<TestOfStudent> _expectedArrayAfterNameIvanFlag = new List<TestOfStudent>
            {
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5),
            };
        private static List<TestOfStudent> _expectedArrayAfterMinMarkThreeFlag = new List<TestOfStudent>
            {
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5),
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3),
                new TestOfStudent("Oleg", "Russian", new DateTime(2019,12,13),4),
                new TestOfStudent("Oleg", "English", new DateTime(2019,12,20),5)
            };
        private static List<TestOfStudent> _expectedArrayAfterMaxMarkThreeFlag = new List<TestOfStudent>
            {
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Olga", "English", new DateTime(2015,12,11),2),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3)
            };
        private static List<TestOfStudent> _expectedArrayAfterDateFromFlag = new List<TestOfStudent>
            {
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4),
                new TestOfStudent("Oleg", "Russian", new DateTime(2019,12,13),4),
                new TestOfStudent("Oleg", "English", new DateTime(2019,12,20),5)
            };
        private static List<TestOfStudent> _expectedArrayAfterDateToFlag = new List<TestOfStudent>
            {
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5),
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Olga", "English", new DateTime(2015,12,11),2),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3)
            };
        private static List<TestOfStudent> _expectedArrayAfterTestMathFlag = new List<TestOfStudent>
            {
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5),
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4)
            };
        private static List<TestOfStudent> _expectedArrayAfterSortAscByMark = new List<TestOfStudent>
            {
                new TestOfStudent("Olga", "English", new DateTime(2015,12,11),2),
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3),
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4),
                new TestOfStudent("Oleg", "Russian", new DateTime(2019,12,13),4),
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5),
                new TestOfStudent("Oleg", "English", new DateTime(2019,12,20),5)
            };
        private static List<TestOfStudent> _expectedArrayAfterSortDescByMark = new List<TestOfStudent>
            {
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5),
                new TestOfStudent("Oleg", "English", new DateTime(2019,12,20),5),
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4),
                new TestOfStudent("Oleg", "Russian", new DateTime(2019,12,13),4),
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3),
                new TestOfStudent("Olga", "English", new DateTime(2015,12,11),2),
            };
        private static List<TestOfStudent> _expectedArrayAfterSortAscByDate = new List<TestOfStudent>
            {
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3),
                new TestOfStudent("Olga", "English", new DateTime(2015,12,11),2),
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4),
                new TestOfStudent("Oleg", "Russian", new DateTime(2019,12,13),4),
                new TestOfStudent("Oleg", "English", new DateTime(2019,12,20),5)
            };
        private static List<TestOfStudent> _expectedArrayAfterSortDescByDate = new List<TestOfStudent>
            {
                new TestOfStudent("Oleg", "English", new DateTime(2019,12,20),5),
                new TestOfStudent("Oleg", "Russian", new DateTime(2019,12,13),4),
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4),
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Olga", "English", new DateTime(2015,12,11),2),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3),
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5)
            };
        private static List<TestOfStudent> _expectedArrayAfterNameOlegAndMinMarkFiveFlags = new List<TestOfStudent>
            {
                new TestOfStudent("Oleg", "English", new DateTime(2019,12,20),5),
                new TestOfStudent("Oleg", "Russian", new DateTime(2019,12,13),4),
                new TestOfStudent("Oleg", "Math", new DateTime(2019,12,12),4),
                new TestOfStudent("Vanya", "Russian", new DateTime(2018,09,13),3),
                new TestOfStudent("Olga", "English", new DateTime(2015,12,11),2),
                new TestOfStudent("Danya", "English", new DateTime(2012,10,5),3),
                new TestOfStudent("Ivan", "Math", new DateTime(2012, 06, 12), 5)
            };
    }
}
