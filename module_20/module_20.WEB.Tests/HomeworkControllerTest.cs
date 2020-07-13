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
    class HomeworkControllerTest
    {
        private Mock<IHomeworkService> Service { get; set; }
        private Mock<ILogger<HomeworkController>> Logger { get; set; }
        private HomeworkController Controller { get; set; }

        [SetUp]
        public void Setup()
        {
            var entities = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    LectureId = 1,
                    Lecture = new Lecture {Id = 1, CourseId = 1, Name ="Test"},
                    StudentId = 1,
                    Mark = 5,
                    Task = "Test"
                },
                new Homework
                {
                    Id = 2,
                    LectureId = 2,
                    Lecture = new Lecture {Id = 2, CourseId = 2, Name ="Test2"},
                    StudentId = 2,
                    Mark = 5,
                    Task = "Test2"
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

            Service = new Mock<IHomeworkService>();

            Logger = new Mock<ILogger<HomeworkController>>();

            Service.Setup(x => x.GetAllHomeworks())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllHomeworksWithLecture())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllHomeworksWithStudent())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllHomeworksWithLectureAndStudent())
                .Returns(collection.Object);

            Service.Setup(x => x.GetHomeworkById(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetHomeworkWithLectureById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetHomeworkWithStudentById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetHomeworkWithLectureAndStudentById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.CreateHomework(It.IsAny<Homework>()))
                .Callback((Homework homework) => entities.Add(homework));

            Service.Setup(x => x.UpdateHomework(It.IsAny<Homework>()))
                .Callback((Homework homework) => Update(homework, entities));

            Service.Setup(x => x.DeleteHomework(It.IsAny<Homework>()))
                .Callback((Homework homework) => entities.RemoveAt(entities.FindIndex(x => x.Id == homework.Id)));

            Service.Setup(x => x.GetAverageMark(1, 1))
                .Returns(Task.Run(()=> (double)entities.ElementAt(0).Mark));

            Controller = new HomeworkController(Service.Object, mapper, Logger.Object);
        }

        private static void Update(Homework homework, List<Homework> entities)
        {
            homework.Lecture = new Lecture { Id = homework.LectureId, Name = "Test", CourseId = homework.LectureId };
            homework.Student = new Student { Id = homework.StudentId, Name = "Test" };
            entities[entities.FindIndex(x => x.Id == homework.Id)] = homework;
        }

        [Test]
        public async Task GetAllHomeworks_Test()
        {
            // Act
            var result = await Controller.GetAllHomeworks();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<HomeworkWithStudentsAndLectureResource>;
            // Assert
            Service.Verify(x => x.GetAllHomeworksWithLectureAndStudent(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkById_Test(int id)
        {
            // Act
            var result = await Controller.GetHomeworkById(id);
            var okObject = result.Result as OkObjectResult;
            var homework = okObject.Value as HomeworkWithStudentsAndLectureResource;
            // Assert
            Service.Verify(x => x.GetHomeworkWithLectureAndStudentById(It.IsAny<int>()), Times.Once);
            Assert.That(homework.Task, Is.EqualTo("Test"));
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkByLectureId_Test(int lectureId)
        {
            // Act
            var result = await Controller.GetHomeworksByLectureId(lectureId);
            var okObject = result.Result as OkObjectResult;
            var homework = okObject.Value as IEnumerable<HomeworkWitnStudentsResource>;
            // Assert
            Service.Verify(x => x.GetAllHomeworksWithLectureAndStudent(), Times.Once);
            Assert.That(homework.Count(), Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkByStudentId_Test(int studentId)
        {
            // Act
            var result = await Controller.GetAllHomeworksByStudentId(studentId);
            var okObject = result.Result as OkObjectResult;
            var homework = okObject.Value as IEnumerable<HomeworkWithLectureResource>;
            // Assert
            Service.Verify(x => x.GetAllHomeworksWithLectureAndStudent(), Times.Once);
            Assert.That(homework.Count(), Is.EqualTo(1));
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkByCourseId_Test(int courseId)
        {
            // Act
            var result = await Controller.GetAllHomeworksByCourseId(courseId);
            var okObject = result.Result as OkObjectResult;
            var homework = okObject.Value as IEnumerable<HomeworkWithStudentsAndLectureResource>;
            // Assert
            Service.Verify(x => x.GetAllHomeworksWithLectureAndStudent(), Times.Once);
            Assert.That(homework.Count(), Is.EqualTo(1));
        }

        [Test]
        [TestCase(1,1)]
        public async Task GetAllHomeworksByCourseIdAndStudentId_Test(int courseId, int studentId)
        {
            // Act
            var result = await Controller.GetAllHomeworksByCourseIdAndStudentId(courseId, studentId);
            var okObject = result.Result as OkObjectResult;
            var homework = okObject.Value as IEnumerable<HomeworkWithLectureResource>;
            // Assert
            Service.Verify(x => x.GetAllHomeworksWithLectureAndStudent(), Times.Once);
            Assert.That(homework.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task CreateHomework_NormalConditions_Test()
        {
            // Arrange
            var newHomework = new SaveHomeworkResource { LectureId = 1, StudentId = 1, Mark = 5, Task = "New" };
            // Act
            await Controller.CreateHomework(newHomework);
            var result = await Controller.GetAllHomeworks();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<HomeworkWithStudentsAndLectureResource>;
            // Assert 
            Service.Verify(x => x.CreateHomework(It.IsAny<Homework>()), Times.Once);
            Assert.That(collection.ElementAt(2).Task, Is.EqualTo("New"));
        }

        [Test]
        public async Task CreateHomework_LectureIdEmptyValidationError_Test()
        {
            // Arrange
            var newHomework = new SaveHomeworkResource { StudentId = 1, Mark = 5, Task = "New" };
            // Act
            var result = await Controller.CreateHomework(newHomework);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        public async Task CreateHomework_StudentIdEmptyValidationError_Test()
        {
            // Arrange
            var newHomework = new SaveHomeworkResource { LectureId = 1, Mark = 5, Task = "New" };
            // Act
            var result = await Controller.CreateHomework(newHomework);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateCourse_NormalConditions_Test(int id)
        {
            // Arrange
            var updateHomework = new SaveHomeworkResource
            {
                LectureId = 1,
                StudentId = 1,
                Mark = 5,
                Task = "Update"
            };
            // Act
            await Controller.UpdateHomework(id, updateHomework);
            // Act
            var result = await Controller.GetAllHomeworks();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<HomeworkWithStudentsAndLectureResource>;
            // Assert 
            Service.Verify(x => x.UpdateHomework(It.IsAny<Homework>()), Times.Once);
            Assert.That(collection.ElementAt(0).Task, Is.EqualTo("Update"));
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateCourse_LectureIdEmptyValidationErrror_Test(int id)
        {
            // Arrange
            var updateHomework = new SaveHomeworkResource
            {
                StudentId = 1,
                Mark = 5,
                Task = "Update"
            };
            // Act
            var result = await Controller.UpdateHomework(id, updateHomework);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateCourse_StudentIdEmptyValidationErrror_Test(int id)
        {
            // Arrange
            var updateHomework = new SaveHomeworkResource
            {
                LectureId = 1,
                Mark = 5,
                Task = "Update"
            };
            // Act
            var result = await Controller.UpdateHomework(id, updateHomework);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(0)]
        public async Task UpdateCourse_IdLowerThanOneValidationError_Test(int id)
        {
            // Arrange
            var updateHomework = new SaveHomeworkResource
            {
                LectureId = 1,
                StudentId = 1,
                Mark = 5,
                Task = "Update"
            };
            // Act
            var result = await Controller.UpdateHomework(id, updateHomework);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteHomework_Test(int id)
        {
            // Act
            await Controller.DeleteHomework(1);
            var result = await Controller.GetAllHomeworks();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<HomeworkWithStudentsAndLectureResource>;
            // Assert 
            Service.Verify(x => x.DeleteHomework(It.IsAny<Homework>()), Times.Once);
            Assert.That(collection.ElementAt(0).Task, Is.EqualTo("Test2"));
        }

        [Test]
        public async Task SetMarkToHomeworks_Test()
        {
            // Arrange
            var marks = new List<SetMarkHomeworkResource>
            {
                new SetMarkHomeworkResource
                {
                    Id = 2,
                    Mark = 0
                }
            };
            Service.Setup(x => x.GetAverageMark(2, 2))
                .Returns(Task.Run(() => 5.0));
            // Act
            await Controller.SetMarkToHomeworks(marks);
            var result = await Controller.GetAllHomeworks();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<HomeworkWithStudentsAndLectureResource>;
            // Assert 
            Service.Verify(x => x.UpdateHomework(It.IsAny<Homework>()), Times.Exactly(marks.Count()));
            Assert.That(collection.ElementAt(1).Mark, Is.EqualTo(0));
        }
    }
}
