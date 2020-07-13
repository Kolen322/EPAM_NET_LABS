using NUnit.Framework;
using Moq;
using module_20.BLL.Interfaces;
using module_20.BLL.Services;
using module_20.DAL.Interfaces;
using module_20.DAL.Entities;
using System.Collections.Generic;
using module_20.BLL.Infrastructure.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace module_20.BLL.Tests
{
    class LecturerServiceTest
    {
        private Mock<ILecturerRepository> Repository { get; set; }
        private Mock<IUnitOfWork> UnitOfWork { get; set; }
        private ILecturerService Service { get; set; }
        private Mock<ILogger<LecturerService>> Logger { get; set; }

        [SetUp]
        public void Setup()
        {
            Repository = new Mock<ILecturerRepository>();

            UnitOfWork = new Mock<IUnitOfWork>();

            Logger = new Mock<ILogger<LecturerService>>();

            UnitOfWork.SetupGet(x => x.Lecturers)
                .Returns(Repository.Object);

            Service = new LecturerService(UnitOfWork.Object, Logger.Object);
        }

        [Test]
        public void GetAll_Test()
        {
            // Arrange
            Repository.Setup(x => x.GetAll())
                .Returns(_testLecturers.AsQueryable());
            // Act
            var lecturers = Service.GetAllLecturers();
            // Assert
            Repository.Verify(x => x.GetAll(), Times.Once);
            Assert.That(lecturers.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllWithCourses_Test()
        {
            // Arrange
            var lecturersTest = new List<Lecturer>(_testLecturers);
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name ="Test",
                    LecturerId = 1
                }
            };
            lecturersTest.ElementAt(0).Courses = courses;

            Repository.Setup(x => x.GetLecturersWithCourses())
                .Returns(lecturersTest.AsQueryable());
            // Act
            var result = Service.GetAllLecturersWithCourses();
            // Assert
            Repository.Verify(x => x.GetLecturersWithCourses(), Times.Once);
            Assert.That(lecturersTest.Count() == 2 && lecturersTest.ElementAt(0).Courses != null);
        }

        [Test]
        [TestCase(1)]
        public async Task GetLecturerById_NormalConditions_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testLecturers.Find(c => c.Id == id));
            // Act
            var lecturer = await Service.GetLecturerById(id);
            // Assert
            Repository.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.That(lecturer.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(4)]
        public void GetLecturerById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testLecturers.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetLecturerById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetLecturerWithCoursesById_NormalConditions_Test(int id)
        {
            // Arrange
            var lecturersTest = new List<Lecturer>(_testLecturers);
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name ="Test",
                    LecturerId = 1
                }
            };
            lecturersTest.ElementAt(0).Courses = courses;
            Repository.Setup(x => x.GetLecturerWithCoursesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturersTest.Find(c => c.Id == id));
            // Act
            var lecturer = await Service.GetLecturerWithCoursesById(id);
            // Assert
            Repository.Verify(x => x.GetLecturerWithCoursesByIdAsync(id), Times.Once);
            Assert.That(lecturer.Id == id && lecturersTest.ElementAt(0).Courses != null);
        }

        [Test]
        [TestCase(4)]
        public void GetLecturerWithCoursesById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var lecturersTest = new List<Lecturer>(_testLecturers);
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name ="Test",
                    LecturerId = 1
                }
            };
            lecturersTest.ElementAt(0).Courses = courses;
            Repository.Setup(x => x.GetLecturerWithCoursesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturersTest.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetLecturerWithCoursesById(id));
        }

        [Test]
        public void CreateLecturer_NormalConditions_Test()
        {
            // Arrange
            var testCreate = new List<Lecturer>(_testLecturers);
            var lecturer = new Lecturer { Id = 3, Name = "Test" };

            Repository.Setup(x => x.AddAsync(It.IsAny<Lecturer>()))
                .Callback((Lecturer lecturer) => testCreate.Add(lecturer));
            // Act
            Service.CreateLecturer(lecturer);
            // Assert
            Repository.Verify(x => x.AddAsync(It.IsAny<Lecturer>()), Times.Once);
            Assert.That(3, Is.EqualTo(testCreate.Count()));
        }

        [Test]
        public void CreateLecturer_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.CreateLecturer(null));
        }

        [Test]
        public void CreateLecturer_EntityAlreadyExistException_Test()
        {
            // Arrange
            var testCreate = new List<Lecturer>(_testLecturers);
            var lecturer = testCreate.ElementAt(0);

            Repository.Setup(x => x.AddAsync(It.IsAny<Lecturer>()))
                .Callback((Lecturer lecturer) => testCreate.Add(lecturer));
            Repository.Setup(x => x.Contains(It.IsAny<Lecturer>()))
               .ReturnsAsync((Lecturer lecturer) => testCreate.Contains(lecturer));
            // Assert
            Assert.ThrowsAsync<EntityAlreadyExistException>(async () => await Service.CreateLecturer(lecturer));
        }

        [Test]
        public void DeleteLecturer_NormalConditions_Test()
        {
            // Arrange
            var testRemove = new List<Lecturer>(_testLecturers);

            Repository.Setup(x => x.Remove(It.IsAny<Lecturer>()))
                .Callback((Lecturer lecturer) => testRemove.RemoveAt(testRemove.FindIndex(x => x.Id == lecturer.Id)));
            Repository.Setup(x => x.Contains(It.IsAny<Lecturer>()))
                .ReturnsAsync((Lecturer lecturer) => testRemove.Contains(lecturer));
            // Act
            var lecturer = testRemove.ElementAt(0);
            Service.DeleteLecturer(lecturer);
            // Assert
            Repository.Verify(x => x.Remove(It.IsAny<Lecturer>()), Times.Once);
            Assert.That(1, Is.EqualTo(testRemove.Count()));
        }

        [Test]
        public void DeleteLecturer_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.DeleteLecturer(null));
        }

        [Test]
        public void DeleteLecturer_EntityNotFoundException_Test()
        {
            // Assert
            var lecturer = new Lecturer { Id = 10, Name = "Test"};
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.DeleteLecturer(lecturer));
        }

        [Test]
        public void UpdateLecturer_NormalConditions_Test()
        {
            // Arrange
            var testUpdate = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Lecturer>()))
               .Callback((Lecturer lecturer) => testUpdate[testUpdate.FindIndex(x => x.Id == lecturer.Id)] = lecturer);
            var updateLecturer = new Lecturer { Id = 1, Name = "update" };
            // Act
            Service.UpdateLecturer(updateLecturer);
            // Assert
            Repository.Verify(x => x.Update(It.IsAny<Lecturer>()), Times.Once);
            Assert.That("update", Is.EqualTo(testUpdate.ElementAt(0).Name));
        }

        [Test]
        public void UpdateLecturer_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.UpdateLecturer(null));
        }

        [Test]
        public void UpdateLecturer_EntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Lecturer>()))
               .Callback((Lecturer lecturer) => testUpdate[testUpdate.FindIndex(x => x.Id == lecturer.Id)] = lecturer);
            var updateLecturer = new Lecturer { Id = 4, Name = "update" };
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateLecturer(updateLecturer));
        }

        public List<Lecturer> _testLecturers = new List<Lecturer>
        {
            new Lecturer
            {
                Id = 1,
                Name = "Test"
            },
            new Lecturer
            {
                Id = 2,
                Name = "Test"
            }
        };
    }
}
