using NUnit.Framework;
using Moq;
using module_20.BLL.Interfaces;
using module_20.DAL.Entities;
using System.Collections.Generic;
using System;
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
    class LectureControllerTest
    {
        private Mock<ILectureService> Service { get; set; }
        private Mock<ILogger<LectureController>> Logger { get; set; }
        private LectureController Controller { get; set; }

        [SetUp]
        public void Setup()
        {
            var entities = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                    Course = new Course {Id = 1, Name ="Test"},
                    Name = "Test",
                    StudentLectures = new List<StudentLecture>
                    {
                        new StudentLecture
                        {
                            StudentId = 1,
                            LectureId = 1,
                            Attendance = false
                        }
                    },
                    Homeworks = new List<Homework>()
                    
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 2,
                    Course = new Course {Id = 2, Name ="Test2"},
                    Name = "Test2",
                    StudentLectures = new List<StudentLecture>
                    {
                        new StudentLecture
                        {
                            StudentId = 2,
                            LectureId = 2,
                            Attendance = true
                        },
                        new StudentLecture
                        {
                            StudentId = 1,
                            LectureId = 2,
                            Attendance = true
                        }
                    },
                    Homeworks = new List<Homework>()
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

            Service = new Mock<ILectureService>();

            Logger = new Mock<ILogger<LectureController>>();

            Service.Setup(x => x.GetAllLectures())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllLecturesWithCourse())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllLecturesWithHomeworks())
                .Returns(collection.Object);

            Service.Setup(x => x.GetAllLecturesWithStudents())
                .Returns(collection.Object);

            Service.Setup(x => x.GetLectureById(It.IsAny<int>()))
                .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetLectureWithCourseById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetLectureWithHomeworksById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
               .Returns((int id) => Task.Run(() => entities.Find(t => t.Id == id)));

            Service.Setup(x => x.CreateLecture(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => entities.Add(lecture));

            Service.Setup(x => x.UpdateLecture(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => entities[entities.FindIndex(x => x.Id == lecture.Id)] = lecture);

            Service.Setup(x => x.DeleteLecture(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => entities.RemoveAt(entities.FindIndex(x => x.Id == lecture.Id)));

            Service.Setup(x => x.AddHomeworkToLecture(It.IsAny<int>(), It.IsAny<string>()))
                .Callback((int id, string task) =>
                    entities[entities.FindIndex(x => x.Id == id)].Homeworks.Add(new Homework { LectureId = id, Task = task }));

            Service.Setup(x => x.MarkAttendance(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .Callback((int id, IEnumerable<int> studentIds) => MarkAttendance(id, studentIds, entities));

            Service.Setup(x => x.MarkAbsence(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .Callback((int id, IEnumerable<int> studentIds) => MarkAbsence(id, studentIds, entities));

            Service.Setup(x => x.GetLecturerOfLecture(It.IsAny<int>()))
                .ReturnsAsync(new Lecturer { Id = 1, Name = "Q" });

            Service.Setup(x => x.GetNumberOfStudentLecturesMissed(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(1);

            Controller = new LectureController(Service.Object, mapper, Logger.Object);
        }

        private static void MarkAttendance(int id, IEnumerable<int> studentIds, List<Lecture> collection)
        {
            var lecture = collection.FirstOrDefault(l => l.Id == id);
            foreach (var studentId in studentIds)
            {
                lecture.StudentLectures
                    .FirstOrDefault(s => s.StudentId == studentId)
                    .Attendance = true;
            }
        }

        private static void MarkAbsence(int id, IEnumerable<int> studentIds, List<Lecture> collection)
        {
            var lecture = collection.FirstOrDefault(l => l.Id == id);
            foreach (var studentId in studentIds)
            {
                lecture.StudentLectures
                    .FirstOrDefault(s => s.StudentId == studentId)
                    .Attendance = false;
            }
        }

        [Test]
        public async Task GetAllLecture_Test()
        {
            // Act
            var result = await Controller.GetAllLectures();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LectureWithCourseResource>;
            // Assert
            Service.Verify(x => x.GetAllLecturesWithCourse(), Times.Once);
            Assert.That(collection.Count(), Is.EqualTo(2));
        }

        [Test]
        [TestCase(1)]
        public async Task GetLectureById_Test(int id)
        {
            // Act
            var result = await Controller.GetLectureById(id);
            var okObject = result.Result as OkObjectResult;
            var lecture = okObject.Value as LectureWithCourseResource;
            // Assert
            Service.Verify(x => x.GetLectureWithCourseById(It.IsAny<int>()), Times.Once);
            Assert.That(lecture.Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task CreateLecture_NormalConditions_Test()
        {
            // Arrange
            var newLecture = new SaveLectureResource { CourseId = 3, Name = "New", Date = new DateTime(1998,12,12) };
            // Act
            await Controller.CreateLecture(newLecture);
            var result = await Controller.GetAllLectures();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LectureWithCourseResource>;
            // Assert 
            Service.Verify(x => x.CreateLecture(It.IsAny<Lecture>()), Times.Once);
            Assert.That(collection.ElementAt(2).Name, Is.EqualTo("New"));
        }

        [Test]
        public async Task CreateLecture_CourseIdEmptyValidationError_Test()
        {
            // Arrange
            var newLecture = new SaveLectureResource { Name = "New", Date = new DateTime(1998, 12, 12) };
            // Act
            var result = await Controller.CreateLecture(newLecture);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateLecture_NormalConditions_Test(int id)
        {
            // Arrange
            var newLecture = new SaveLectureResource { CourseId = 1, Name = "Update", Date = new DateTime(1998, 12, 12) };
            // Act
            await Controller.UpdateLecture(id, newLecture);
            var result = await Controller.GetAllLectures();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LectureWithCourseResource>;
            // Assert 
            Service.Verify(x => x.UpdateLecture(It.IsAny<Lecture>()), Times.Once);
            Assert.That(collection.ElementAt(0).Name, Is.EqualTo("Update"));
        }

        [Test]
        [TestCase(1)]
        public async Task UpdateLecture_CourseIdEmptyValidationError_Test(int id)
        {
            // Arrange
            var newLecture = new SaveLectureResource {Name = "Update", Date = new DateTime(1998, 12, 12) };
            // Act
            var result = await Controller.UpdateLecture(id, newLecture);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(0)]
        public async Task UpdateLecture_IdLowerThanOneValidationError_Test(int id)
        {
            // Arrange
            var newLecture = new SaveLectureResource { CourseId = 1, Name = "Update", Date = new DateTime(1998, 12, 12) };
            // Act
            var result = await Controller.UpdateLecture(id, newLecture);
            var badRequest = result.Result as BadRequestObjectResult;
            // Assert
            Assert.That(badRequest is BadRequestObjectResult);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteLecture_Test(int id)
        {
            // Act
            await Controller.DeleteLecture(id);
            var result = await Controller.GetAllLectures();
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<LectureWithCourseResource>;
            // Assert 
            Service.Verify(x => x.DeleteLecture(It.IsAny<Lecture>()), Times.Once);
            Assert.That(collection.ElementAt(0).Name, Is.EqualTo("Test2"));
        }

        [Test]
        [TestCase("Test2")]
        public async Task GetAttendanceOfLectureByName_Test(string name)
        {
            // Act
            var result = await Controller.GetAttendanceOfLectureByName(name);
            var okObject = result.Result as OkObjectResult;
            var attendace = okObject.Value as IEnumerable<AttendanceOfLectureResource>;
            // Assert
            Service.Verify(x => x.GetAllLecturesWithStudents(), Times.Once);
            Assert.That(attendace.Count(), Is.EqualTo(2));
        }

        [Test]
        [TestCase(1)]
        public async Task AddHomeworkToLecture_Test(int id)
        {
            // Act
            var result = await Controller.AddHomeworkToLecture(id, "Task");
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable <HomeworkWithLectureResource>;
            // Assert 
            Service.Verify(x => x.AddHomeworkToLecture(It.IsAny<int>(),It.IsAny<string>()), Times.Once);
            Assert.That(collection.ElementAt(0).Task, Is.EqualTo("Task"));
        }

        [Test]
        [TestCase(1)]
        public async Task MarkAttendance_Test(int id)
        {
            // Arrange
            var studentIds = new List<int> { 1 };
            // Act
            var result = await Controller.MarkAttendance(id, studentIds);
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<AttendanceOfLectureResource>;
            // Assert 
            Service.Verify(x => x.MarkAttendance(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()), Times.Once);
            Assert.That(collection.ElementAt(0).Attendance,Is.EqualTo(true));
        }

        [Test]
        [TestCase(2)]
        public async Task MarkAbsence_Test(int id)
        {
            // Arrange
            var studentIds = new List<int> { 2 };
            // Act
            var result = await Controller.MarkAbsence(id, studentIds);
            var okObject = result.Result as OkObjectResult;
            var collection = okObject.Value as IEnumerable<AttendanceOfLectureResource>;
            // Assert 
            Service.Verify(x => x.MarkAbsence(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()), Times.Once);
            Assert.That(collection.ElementAt(0).Attendance, Is.EqualTo(false));
        }
    }
}
