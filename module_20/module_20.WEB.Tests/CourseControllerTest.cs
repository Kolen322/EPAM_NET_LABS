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
    class CourseControllerTest
    {
        private Mock<ICourseService> Service { get; set; }
        private Mock<ILogger<CourseController>> Logger { get; set; }
        private CourseController Controller { get; set; }

        [SetUp]
        public void Setup()
        {
            var entities = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test",
                    StudentCourses = new List<StudentCourse>()
                },
                new Course
                {
                    Id = 2,
                    Name = "Test2",
                    StudentCourses = new List<StudentCourse>()
                }
            };


            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new CourseProfile());
            });

            var collection = entities.AsQueryable().BuildMock();

            var mapper = config.CreateMapper();

            Service = new Mock<ICourseService>();

            Logger = new Mock<ILogger<CourseController>>();

            Service.Setup(x => x.GetAllCourses())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllCoursesWithStudents())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllCoursesWithLecturer())
                .Returns(collection.Object);

            Service.Setup(x => x.GetCourseById(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetCourseWithLecturerById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetCourseWithStudentsById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.CreateCourse(It.IsAny<Course>()))
                .Callback((Course course) => entities.Add(course));

            Service.Setup(x => x.UpdateCourse(It.IsAny<Course>()))
                .Callback((Course course) => entities[entities.FindIndex(x => x.Id == course.Id)] = course);

            Service.Setup(x => x.DeleteCourse(It.IsAny<Course>()))
                .Callback((Course course) => entities.RemoveAt(entities.FindIndex(x => x.Id == course.Id)));

            Service.Setup(x => x.AddStudentToCourse(It.IsAny<int>(), It.IsAny<int>()))
                .Callback((int courseId, int studentId) =>
                    entities[entities.FindIndex(x => x.Id == courseId)].StudentCourses.Add(new StudentCourse { CourseId = courseId, StudentId = studentId }));

            Controller = new CourseController(Service.Object, mapper, Logger.Object);
        }

        [Test]
        public async Task GetAllCourses_Test()
        {
            // Act
            var result = await Controller.GetAllCourses();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<CourseResource>;
            // Assert
            Service.Verify(x => x.GetAllCourses(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllCoursesWithStudents_Test()
        {
            // Act
            var result = await Controller.GetAllWithStudents();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<CourseWithStudentsResource>;
            // Assert
            Service.Verify(x => x.GetAllCoursesWithStudents(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllCoursesWithLecturer_Test()
        {
            // Act
            var result = await Controller.GetAllWithLecturer();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<CourseWithLecturerResource>;
            // Assert
            Service.Verify(x => x.GetAllCoursesWithLecturer(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseById_Test(int id)
        {
            // Act
            var result = await Controller.GetCourseById(id);
            var okObject = result.Result as OkObjectResult;
            var course = okObject.Value as CourseResource;
            // Assert
            Service.Verify(x => x.GetCourseById(It.IsAny<int>()), Times.Once);
            Assert.That(course.Name, Is.EqualTo("Test"));
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseWithStudentsById_Test(int id)
        {
            // Act
            var result = await Controller.GetCourseWithStudentsById(id);
            var okObject = result.Result as OkObjectResult;
            var course = okObject.Value as CourseWithStudentsResource;
            // Assert
            Service.Verify(x => x.GetCourseWithStudentsById(It.IsAny<int>()), Times.Once);
            Assert.That(course.Name, Is.EqualTo("Test"));
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseWithLecturerById_Test(int id)
        {
            // Act
            var result = await Controller.GetCourseWithLecturerById(id);
            var okObject = result.Result as OkObjectResult;
            var course = okObject.Value as CourseWithLecturerResource;
            // Assert
            Service.Verify(x => x.GetCourseWithLecturerById(It.IsAny<int>()), Times.Once);
            Assert.That(course.Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task CreateCourse_NormalConditions_Test()
        {
            // Arrange
            var newCourse = new SaveCourseResource { Name = "New", LecturerId = 1 };
            // Act
            await Controller.CreateCourse(newCourse);
            var result = await Controller.GetAllCourses();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<CourseResource>;
            // Assert 
            Service.Verify(x => x.CreateCourse(It.IsAny<Course>()), Times.Once);
            Assert.That(collection.ElementAt(2).Name, Is.EqualTo("New"));
        }

        [Test]
        public async Task CreateCourse_LecturerIdEmptyValidationError_Test()
        {
            // Arrange
            var newCourse = new SaveCourseResource { Name = "New" };
            // Act
            var result = await Controller.CreateCourse(newCourse);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateCourse_NormalConditions_Test(int id)
        {
            // Arrange
            var updateCourse = new SaveCourseResource
            {
                Name = "Update",
                LecturerId = 1
            };
            // Act
            await Controller.UpdateCourse(id, updateCourse);
            var result = await Controller.GetAllCourses();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<CourseResource>;
            // Assert 
            Service.Verify(x => x.UpdateCourse(It.IsAny<Course>()), Times.Once);
            Assert.That(collection.ElementAt(0).Name, Is.EqualTo("Update"));
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateCourse_LecturerIdEmptyValidationError_Test(int id)
        {
            // Arrange
            var updateCourse = new SaveCourseResource
            {
                Name = "Update"
            };
            // Act
            var result = await Controller.UpdateCourse(id, updateCourse);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(0)]
        public async Task UpdateCourse_IdLowerThanOneValidationError_Test(int id)
        {
            // Arrange
            var updateCourse = new SaveCourseResource
            {
                Name = "Update"
            };
            // Act
            var result = await Controller.UpdateCourse(id, updateCourse);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteCourse_Test(int id)
        {
            // Act
            await Controller.DeleteCourse(1);
            var result = await Controller.GetAllCourses();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<CourseResource>;
            // Assert 
            Service.Verify(x => x.DeleteCourse(It.IsAny<Course>()), Times.Once);
            Assert.That(collection.ElementAt(0).Name, Is.EqualTo("Test2"));
        }

        [Test]
        [TestCase(1,1)]
        public async Task AddStudentToCourse_Test(int courseId, int studentId)
        {
            // Act
            await Controller.AddStudentToCourse(courseId, studentId);
            var result = await Controller.GetAllWithStudents();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<CourseWithStudentsResource>;
            // Assert 
            Service.Verify(x => x.AddStudentToCourse(It.IsAny<int>(),It.IsAny<int>()), Times.Once);
            Assert.That(collection.ElementAt(0).Students.Count(), Is.EqualTo(1));
        }
    }
}
