using NUnit.Framework;
using Moq;
using module_20.BLL.Interfaces;
using module_20.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using module_20.Web.Controllers;
using AutoMapper;
using module_20.Web.Resources;
using Microsoft.Extensions.Logging;
using MockQueryable.Moq;
using Microsoft.AspNetCore.Mvc;
using module_20.Web.Mapping;

namespace module_20.WEB.Tests
{
    class LecturerControllerTest
    {
        private Mock<ILecturerService> Service { get; set; }
        private Mock<ILogger<LecturerController>> Logger { get; set; }
        private LecturerController Controller { get; set; }

        [SetUp]
        public void Setup()
        {
            var entities = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name = "Test"
                },
                new Lecturer
                {
                    Id = 2,
                    Name = "Test2"
                }
            };


            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new LecturerProfile());
            });

            var collection = entities.AsQueryable().BuildMock();

            var mapper = config.CreateMapper();

            Service = new Mock<ILecturerService>();

            Logger = new Mock<ILogger<LecturerController>>();

            Service.Setup(x => x.GetAllLecturers())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllLecturers())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllLecturersWithCourses())
                .Returns(collection.Object);

            Service.Setup(x => x.GetLecturerById(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetLecturerWithCoursesById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.CreateLecturer(It.IsAny<Lecturer>()))
                .Callback((Lecturer lecturer) => entities.Add(lecturer));

            Service.Setup(x => x.UpdateLecturer(It.IsAny<Lecturer>()))
                .Callback((Lecturer lecturer) => entities[entities.FindIndex(x => x.Id == lecturer.Id)] = lecturer);

            Service.Setup(x => x.DeleteLecturer(It.IsAny<Lecturer>()))
                .Callback((Lecturer lecturer) => entities.RemoveAt(entities.FindIndex(x => x.Id == lecturer.Id)));

            Controller = new LecturerController(Service.Object, mapper, Logger.Object);
        }

        [Test]
        public async Task GetAllLecturers_Test()
        {
            // Act
            var result = await Controller.GetAllLecturers();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LecturerResource>;
            // Assert
            Service.Verify(x => x.GetAllLecturers(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllLecturersWithCourse_Test()
        {
            // Act
            var result = await Controller.GetAllLecturersWithCourses();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LecturerWithCourseResource>;
            // Assert
            Service.Verify(x => x.GetAllLecturersWithCourses(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        [TestCase(1)]
        public async Task GetLecturerById_Test(int id)
        {
            // Act
            var result = await Controller.GetLecturerById(id);
            var okObject = result.Result as OkObjectResult;
            var lecturer = okObject.Value as LecturerResource;
            // Assert
            Service.Verify(x => x.GetLecturerById(It.IsAny<int>()), Times.Once);
            Assert.That(lecturer.Name, Is.EqualTo("Test"));
        }

        [Test]
        [TestCase(1)]
        public async Task GetLecturerWithCourseById_Test(int id)
        {
            // Act
            var result = await Controller.GetLecturerWithCourseById(id);
            var okObject = result.Result as OkObjectResult;
            var lecturer = okObject.Value as LecturerWithCourseResource;
            // Assert
            Service.Verify(x => x.GetLecturerWithCoursesById(It.IsAny<int>()), Times.Once);
            Assert.That(lecturer.Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task CreateLecturer_NormalConditions_Test()
        {
            // Arrange
            var newLecturer = new SaveLecturerResource { Name = "New", Email ="test@test.ru", Mobile = "+7 (931) 945-23-45" };
            // Act
            await Controller.CreateLecturer(newLecturer);
            var result = await Controller.GetAllLecturers();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LecturerResource>;
            // Assert 
            Service.Verify(x => x.CreateLecturer(It.IsAny<Lecturer>()), Times.Once);
            Assert.That(collection.ElementAt(2).Name, Is.EqualTo("New"));
        }

        [Test]
        public async Task CreateLecturer_MobileValidationError_Test()
        {
            // Arrange
            var newLecturer = new SaveLecturerResource { Name = "New", Email = "test@test.ru", Mobile = "Q" };
            // Act
            var result = await Controller.CreateLecturer(newLecturer);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        public async Task CreateLecturer_EmailValidationError_Test()
        {
            // Arrange
            var newLecturer = new SaveLecturerResource { Name = "New", Email = "Q", Mobile = "+7 (931) 945-23-45" };
            // Act
            var result = await Controller.CreateLecturer(newLecturer);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateLecturer_NormalConditions_Test(int id)
        {
            // Arrange
            var newLecturer = new SaveLecturerResource { Name = "Update", Email= "test@test.ru", Mobile= "+7 (931) 945-23-45" };
            // Act
            await Controller.UpdateLecturer(id, newLecturer);
            var result = await Controller.GetAllLecturers();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LecturerResource>;
            // Assert 
            Service.Verify(x => x.UpdateLecturer(It.IsAny<Lecturer>()), Times.Once);
            Assert.That(collection.ElementAt(0).Name, Is.EqualTo("Update"));
        }

        [Test]
        [TestCase(0)]
        public async Task UpdateLecturer_IdLowerThanOneValidationError_Test(int id)
        {
            // Arrange
            var newLecturer = new SaveLecturerResource { Name = "Update" };
            // Act
            var result = await Controller.UpdateLecturer(id, newLecturer);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteLecturer_Test(int id)
        {
            // Act
            await Controller.DeleteLecturer(id);
            var result = await Controller.GetAllLecturers();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LecturerResource>;
            // Assert 
            Service.Verify(x => x.DeleteLecturer(It.IsAny<Lecturer>()), Times.Once);
            Assert.That(collection.ElementAt(0).Name, Is.EqualTo("Test2"));
        }
    }

}
