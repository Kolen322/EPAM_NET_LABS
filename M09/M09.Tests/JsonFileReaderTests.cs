using NUnit.Framework;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Moq;

namespace M09.Tests
{
    class JsonFileReaderTests
    {
        [Test]
        public void ReadFromJson_NormalConditions_Test()
        {
            string json = JsonConvert.SerializeObject(_testArray);
            var textReader = new Mock<TextReader>();
            textReader.Setup(tr => tr.ReadToEnd()).Returns(json);

            var JsonFileReader = new JsonFileReader(textReader.Object);
            var actualCollection = JsonFileReader.ReadFromJson();

            CollectionAssert.AreEqual(actualCollection, _testArray);
        }

        [Test]
        public void ReadFromJson_JsonReaderException_Test()
        {
            string json = "fff";
            var textReader = new Mock<TextReader>();
            textReader.Setup(tr => tr.ReadToEnd()).Returns(json);

            var JsonFileReader = new JsonFileReader(textReader.Object);

            Assert.Throws<JsonReaderException>(() => JsonFileReader.ReadFromJson());

        }

        [Test]
        public void ReadFromJson_ArgumentNullException_Test()
        {
            string json = null;
            var textReader = new Mock<TextReader>();
            textReader.Setup(tr => tr.ReadToEnd()).Returns(json);

            var JsonFileReader = new JsonFileReader(textReader.Object);

            Assert.Throws<ArgumentNullException>(() => JsonFileReader.ReadFromJson());
        }

        [Test]
        public void ReadFromJson_FileNotFoundException_Test()
        {
            JsonFileReader jsonFileReader = null;
            Assert.Throws<FileNotFoundException>(() => jsonFileReader = new JsonFileReader(new StreamReader("ffffff")));
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
    }


}
