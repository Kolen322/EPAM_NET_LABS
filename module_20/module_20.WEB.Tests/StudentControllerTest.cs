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
    class StudentControllerTest
    {
        private Mock<IStudentService> Service { get; set; }
        private Mock<ILogger<StudentController>> Logger { get; set; }
        private StudentController Controller { get; set; }

        [SetUp]
        public void Setup()
        {
            var entities = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test",
                    StudentLectures = new List<StudentLecture>
                    {
                        new StudentLecture
                        {
                            StudentId = 1,
                            LectureId = 1
                        }
                    }
                },
                new Student
                {
                    Id = 2,
                    Name = "Test2",
                    StudentLectures = new List<StudentLecture>
                    {
                        new StudentLecture
                        {
                            StudentId = 2,
                            LectureId = 2
                        },
                        new StudentLecture
                        {
                            StudentId = 2,
                            LectureId = 3
                        }
                    }
                }
            };


            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new HomeworkProfile());
                opts.AddProfile(new AttendanceProfile());
                opts.AddProfile(new CourseProfile());
                opts.AddProfile(new StudentProfile());
                opts.AddProfile(new LecturerProfile());
                opts.AddProfile(new LectureProfile());
            });

            var collection = entities.AsQueryable().BuildMock();

            var mapper = config.CreateMapper();

            Service = new Mock<IStudentService>();

            Logger = new Mock<ILogger<StudentController>>();

            Service.Setup(x => x.GetAllStudents())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllStudentsWithCourses())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllStudentsWithLectures())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllStudentsWithLecturesAndCourses())
                .Returns(collection.Object);

            Service.Setup(x => x.GetStudentById(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetStudentWithCoursesById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetStudentWithLecturesAndCoursesById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetStudentWithLecturesById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.CreateStudent(It.IsAny<Student>()))
                .Callback((Student student) => entities.Add(student));

            Service.Setup(x => x.UpdateStudent(It.IsAny<Student>()))
                .Callback((Student student) => entities[entities.FindIndex(x => x.Id == student.Id)] = student);

            Service.Setup(x => x.DeleteStudent(It.IsAny<Student>()))
                .Callback((Student student) => entities.RemoveAt(entities.FindIndex(x => x.Id == student.Id)));

            Controller = new StudentController(Service.Object, mapper, Logger.Object);
        }

        [Test]
        public async Task GetAllStudents_Test()
        {
            // Act
            var result = await Controller.GetAllStudents();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<StudentResource>;
            // Assert
            Service.Verify(x => x.GetAllStudents(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllStudentsWithCourses_Test()
        {
            // Act
            var result = await Controller.GetAllStudentsWithCourse();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<StudentWithCoursesResource>;
            // Assert
            Service.Verify(x => x.GetAllStudentsWithCourses(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        [TestCase(1)]
        public async Task GetStudentById_Test(int id)
        {
            // Act
            var result = await Controller.GetStudentById(id);
            var okObject = result.Result as OkObjectResult;
            var student = okObject.Value as StudentResource;
            // Assert
            Service.Verify(x => x.GetStudentById(It.IsAny<int>()), Times.Once);
            Assert.That(student.Name, Is.EqualTo("Test"));
        }

        [Test]
        [TestCase(1)]
        public async Task GetStudentWithCoursesById_Test(int id)
        {
            // Act
            var result = await Controller.GetStudentWithCoursesById(id);
            var okObject = result.Result as OkObjectResult;
            var student = okObject.Value as StudentWithCoursesResource;
            // Assert
            Service.Verify(x => x.GetStudentWithCoursesById(It.IsAny<int>()), Times.Once);
            Assert.That(student.Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task CreateStudent_NormalConditions_Test()
        {
            // Arrange
            var newStudent = new SaveStudentResource { Name = "New", Email = "test@test.ru", Mobile = "+7 (931) 945-23-45" };
            // Act
            await Controller.CreateStudent(newStudent);
            var result = await Controller.GetAllStudents();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<StudentResource>;
            // Assert 
            Service.Verify(x => x.CreateStudent(It.IsAny<Student>()), Times.Once);
            Assert.That(collection.ElementAt(2).Name, Is.EqualTo("New"));
        }

        [Test]
        public async Task CreateCreateStudent_MobileValidationError_Test()
        {
            // Arrange
            var newStudent = new SaveStudentResource { Name = "New", Email = "test@test.ru", Mobile = "Q" };
            // Act
            var result = await Controller.CreateStudent(newStudent);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        public async Task CreateStudent_EmailValidationError_Test()
        {
            // Arrange
            var newStudent = new SaveStudentResource { Name = "New", Email = "Q", Mobile = "+7 (931) 945-23-45" };
            // Act
            var result = await Controller.CreateStudent(newStudent);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateStudent_NormalConditions_Test(int id)
        {
            // Arrange
            var newStudent = new SaveStudentResource { Name = "Update", Email = "test@test.ru", Mobile = "+7 (931) 945-23-45" };
            // Act
            await Controller.UpdateStudent(id, newStudent);
            var result = await Controller.GetAllStudents();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<StudentResource>;
            // Assert 
            Service.Verify(x => x.UpdateStudent(It.IsAny<Student>()), Times.Once);
            Assert.That(collection.ElementAt(0).Name, Is.EqualTo("Update"));
        }

        [Test]
        [TestCase(0)]
        public async Task UpdateStudent_IdLowerThanOneValidationError_Test(int id)
        {
            // Arrange
            var newStudent = new SaveStudentResource { Name = "Update" };
            // Act
            var result = await Controller.UpdateStudent(id, newStudent);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteStudent_Test(int id)
        {
            // Act
            await Controller.DeleteStudent(id);
            var result = await Controller.GetAllStudents();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<StudentResource>;
            // Assert 
            Service.Verify(x => x.DeleteStudent(It.IsAny<Student>()), Times.Once);
            Assert.That(collection.ElementAt(0).Name, Is.EqualTo("Test2"));
        }

        [Test]
        [TestCase("Test2")]
        public async Task GetAttendanceOfStudentByName_Test(string name)
        {
            // Act
            var result = await Controller.GetAttendanceOfStudentByName(name);
            var okObject = result.Result as OkObjectResult;
            var attendace = okObject.Value as IEnumerable<AttendanceOfStudentResource>;
            // Assert
            Service.Verify(x => x.GetAllStudentsWithLectures(), Times.Once);
            Assert.That(attendace.Count(), Is.EqualTo(2));
        }
    }
}
