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
    public class CourseServiceTest
    {
        private List<Course> _testCourses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Kk"
                },
                new Course
                {
                    Id = 2,
                    Name = "Kk"
                }
            };

        private Mock<ICourseRepository> Repository { get; set; }
        private Mock<ILecturerRepository> LecturerRepository { get; set; }
        private Mock<IStudentRepository> StudentRepository { get; set; }
        private Mock<IUnitOfWork> UnitOfWork { get; set; }
        private ICourseService Service { get; set; }
        private Mock<ILogger<CourseService>> Logger { get; set; }

        [SetUp]
        public void Setup()
        {
            Repository = new Mock<ICourseRepository>();
            LecturerRepository = new Mock<ILecturerRepository>();

            StudentRepository = new Mock<IStudentRepository>();

            UnitOfWork = new Mock<IUnitOfWork>();

            Logger = new Mock<ILogger<CourseService>>();

            UnitOfWork.SetupGet(x => x.Courses)
                .Returns(Repository.Object);

            UnitOfWork.SetupGet(x => x.Lecturers)
                .Returns(LecturerRepository.Object);

            UnitOfWork.SetupGet(x => x.Students)
                .Returns(StudentRepository.Object);

            Service = new CourseService(UnitOfWork.Object, Logger.Object);
        }

        [Test]
        public void GetAll_Test()
        {
            // Arrange
            Repository.Setup(x => x.GetAll())
                .Returns(_testCourses.AsQueryable());
            // Act
            var course = Service.GetAllCourses();
            // Assert
            Repository.Verify(x => x.GetAll(), Times.Once);
            Assert.That(course.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllCoursesWithLecturer_Test()
        {
            // Arrange
            var courseWithLecturer = new List<Course>(_testCourses);
            var lecturer = new Lecturer { Id = 1, Email = "test", Name = "test", Mobile = "test" };
            foreach(var course in courseWithLecturer)
            {
                course.LecturerId = 1;
                course.Lecturer = lecturer;
            }
            Repository.Setup(x => x.GetCoursesWithLecturer())
                .Returns(courseWithLecturer.AsQueryable());
            // Act
            var testCourse = Service.GetAllCoursesWithLecturer();
            // Assert
            Repository.Verify(x => x.GetCoursesWithLecturer(), Times.Once);
            Assert.That(testCourse.Count() == 2 && testCourse.ElementAt(1).Lecturer != null);
        }

        [Test]
        public void GetAllCoursesWithLectures_Test()
        {
            // Arrange
            var courseWithLecture = new List<Course>(_testCourses);
            courseWithLecture.ElementAt(1).Lectures = new List<Lecture>
                { new Lecture { Id = 1, CourseId = 1, Name = "Test"} };
            Repository.Setup(x => x.GetCoursesWithLectures())
                .Returns(courseWithLecture.AsQueryable());
            // Act
            var testCourse = Service.GetAllCoursesWithLectures();
            // Assert
            Repository.Verify(x => x.GetCoursesWithLectures(), Times.Once);
            Assert.That(testCourse.Count() == 2 && testCourse.ElementAt(1).Lectures != null);
        }

        [Test]
        public void GetAllCoursesWithStudents_Test()
        {
            // Arrange
            var courseWithStudents = new List<Course>(_testCourses);
            courseWithStudents.ElementAt(1).StudentCourses = new List<StudentCourse>();
            Repository.Setup(x => x.GetCoursesWithStudents())
                .Returns(courseWithStudents.AsQueryable());
            // Act
            var testCourse = Service.GetAllCoursesWithStudents();
            // Assert
            Repository.Verify(x => x.GetCoursesWithStudents(), Times.Once);
            Assert.That(testCourse.Count() == 2 && testCourse.ElementAt(1).StudentCourses != null);
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseById_NormalConditions_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testCourses.Find(c => c.Id == id));
            // Act
            var course = await Service.GetCourseById(id);
            // Assert
            Repository.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.That(course.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(4)]
        public void GetCourseById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testCourses.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetCourseById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseWithLecturerById_NormalConditions_Test(int id)
        {
            // Arrange
            var courseWithLecturer = new List<Course>(_testCourses);
            var lecturer = new Lecturer { Id = 1, Email = "test", Name = "test", Mobile = "test" };
            courseWithLecturer.ElementAt(id - 1).Lecturer = lecturer;

            Repository.Setup(x => x.GetCourseWithLecturerById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseWithLecturer.Find(c => c.Id == id));
            // Act
            var course = await Service.GetCourseWithLecturerById(id);
            // Assert
            Repository.Verify(x => x.GetCourseWithLecturerById(id), Times.Once);
            Assert.That(course.Id, Is.EqualTo(id));
            Assert.That(course.Lecturer != null);
        }

        [Test]
        [TestCase(4)]
        public void GetCourseWithLecturerById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var courseWithLecturer = new List<Course>(_testCourses);
            var lecturer = new Lecturer { Id = 1, Email = "test", Name = "test", Mobile = "test" };
            courseWithLecturer.ElementAt(0).Lecturer = lecturer;

            Repository.Setup(x => x.GetCourseWithLecturerById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseWithLecturer.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetCourseWithLecturerById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseWithLecturesById_NormalConditions_Test(int id)
        {
            // Arrange
            var courseWithLecture = new List<Course>(_testCourses);
            courseWithLecture.ElementAt(id - 1).Lectures = new List<Lecture>
                { new Lecture { Id = 1, CourseId = 1, Name = "Test"} };

            Repository.Setup(x => x.GetCourseWithLecturesById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseWithLecture.Find(c => c.Id == id));
            // Act
            var course = await Service.GetCourseWithLecturesById(id);
            // Assert
            Repository.Verify(x => x.GetCourseWithLecturesById(id), Times.Once);
            Assert.That(course.Id, Is.EqualTo(id));
            Assert.That(course.Lectures != null);
        }

        [Test]
        [TestCase(4)]
        public void GetCourseWithLecturesById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var courseWithLecture = new List<Course>(_testCourses);
            courseWithLecture.ElementAt(0).Lectures = new List<Lecture>
                { new Lecture { Id = 1, CourseId = 1, Name = "Test"} };

            Repository.Setup(x => x.GetCourseWithLecturesById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseWithLecture.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetCourseWithLecturesById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseWithStudentsById_NormalConditions_Test(int id)
        {
            // Arrange
            var courseWithStudents = new List<Course>(_testCourses);
            courseWithStudents.ElementAt(id - 1).StudentCourses = new List<StudentCourse>();

            Repository.Setup(x => x.GetCourseWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseWithStudents.Find(c => c.Id == id));
            // Act
            var course = await Service.GetCourseWithStudentsById(id);
            // Assert
            Repository.Verify(x => x.GetCourseWithStudentsById(id), Times.Once);
            Assert.That(course.Id, Is.EqualTo(id));
            Assert.That(course.StudentCourses != null);
        }

        [Test]
        [TestCase(4)]
        public void GetCourseWithStudentsById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var courseWithStudents = new List<Course>(_testCourses);
            courseWithStudents.ElementAt(0).StudentCourses = new List<StudentCourse>();

            Repository.Setup(x => x.GetCourseWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseWithStudents.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetCourseWithStudentsById(id));
        }

        [Test]
        public void CreateCourse_NormalConditions_Test()
        {
            // Arrange
            var testCreate = new List<Course>(_testCourses);
            var course = new Course
            {
                Id = 3,
                Name = "test",
                LecturerId = 1
            };
            var lecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            Repository.Setup(x => x.AddAsync(It.IsAny<Course>()))
                .Callback((Course course) => testCreate.Add(course));

            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturers.Find(l => l.Id == id));
            // Act
            Service.CreateCourse(course);
            // Assert
            Repository.Verify(x => x.AddAsync(It.IsAny<Course>()), Times.Once);
            Assert.That(3, Is.EqualTo(testCreate.Count()));
        }

        [Test]
        public void CreateCourse_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.CreateCourse(null));
        }

        [Test]
        public void CreateCourse_LecturerEntityNotFoundException_Test()
        {
            // Arrange
            var testCreate = new List<Course>(_testCourses);
            var course = new Course
            {
                Id = 3,
                Name = "test",
                LecturerId = 5
            };
            var lecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            Repository.Setup(x => x.AddAsync(It.IsAny<Course>()))
                .Callback((Course course) => testCreate.Add(course));

            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturers.Find(l => l.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.CreateCourse(course));
        }

        [Test]
        public void CreateCourse_EntityAlreadyExistException_Test()
        {
            // Arrange
            var testCreate = new List<Course>(_testCourses);
            testCreate.ElementAt(0).LecturerId = 1;
            var course = testCreate.ElementAt(0);
            var lecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            Repository.Setup(x => x.AddAsync(It.IsAny<Course>()))
                .Callback((Course course) => testCreate.Add(course));

            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturers.Find(l => l.Id == id));
            Repository.Setup(x => x.Contains(It.IsAny<Course>()))
              .ReturnsAsync((Course course) => testCreate.Contains(course));
            // Assert
            Assert.ThrowsAsync<EntityAlreadyExistException>(async () => await Service.CreateCourse(course));
        }

        [Test]
        public void DeleteCourse_NormalConditions_Test()
        {
            // Arrange
            var testRemove = new List<Course>(_testCourses);

            Repository.Setup(x => x.Remove(It.IsAny<Course>()))
                .Callback((Course course) => testRemove.RemoveAt(testRemove.FindIndex(x => x.Id == course.Id)));
            Repository.Setup(x => x.Contains(It.IsAny<Course>()))
                .ReturnsAsync((Course course) => testRemove.Contains(course));
            // Act
            var course = testRemove.ElementAt(0);

            Service.DeleteCourse(course);
            // Assert
            Repository.Verify(x => x.Remove(It.IsAny<Course>()), Times.Once);
            Assert.That(1, Is.EqualTo(testRemove.Count()));
        }

        [Test]
        public void DeleteCourse_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.DeleteCourse(null));
        }

        [Test]
        public void DeleteCourse_EntityNotFoundException_Test()
        {
            // Assert
            var course = new Course { Id = 5, Name = "Test" };
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.DeleteCourse(course));
        }

        [Test]
        public void UpdateCourse_NormalConditions_Test()
        {
            // Arrange
            var testUpdate = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "test",
                    LecturerId = 1
                }
            };

            var lecturersCollection = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };

            Repository.Setup(x=>x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturersCollection.Find(c => c.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Course>()))
                .Callback((Course course) => testUpdate[testUpdate.FindIndex(x => x.Id == course.Id)] = course);

            var updateCourse = new Course { Id = 1, Name = "update", LecturerId = 1 };
            // Act
            Service.UpdateCourse(updateCourse);
            // Assert
            Repository.Verify(x => x.Update(It.IsAny<Course>()), Times.Once);
            Assert.That("update", Is.EqualTo(testUpdate.ElementAt(0).Name));
        }

        [Test]
        public void UpdateCourse_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.UpdateCourse(null));
        }

        [Test]
        public void UpdateCourse_LecturerEntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "test",
                    LecturerId = 1
                }
            };

            var lecturersCollection = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturersCollection.Find(c => c.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Course>()))
                .Callback((Course course) => testUpdate[testUpdate.FindIndex(x => x.Id == course.Id)] = course);

            var updateCourse = new Course { Id = 1, Name = "update", LecturerId = 5 };
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateCourse(updateCourse));
        }

        [Test]
        public void UpdateCourse_EntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "test",
                    LecturerId = 1
                }
            };

            var lecturersCollection = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturersCollection.Find(c => c.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Course>()))
                .Callback((Course course) => testUpdate[testUpdate.FindIndex(x => x.Id == course.Id)] = course);

            var updateCourse = new Course { Id = 10, Name = "update", LecturerId = 1 };
            // Assert
            var course = new Course { Id = 10, Name = "Test" };
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateCourse(course));
        }

        [Test]
        public void AddStudentToCourse_NormalConditions_Test()
        {
            // Arrange
            var courseCollection = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            var studentCollection = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name ="Test"
                }
            };

            Repository.Setup(x => x.GetCourseWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseCollection.Find(c => c.Id == id));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentCollection.Find(c => c.Id == id));
            var expectedStudentCourse = new List<StudentCourse>
            {
                new StudentCourse
                { 
                    CourseId = 1,
                    StudentId = 1
                }
            };
            // Act
            Service.AddStudentToCourse(1, 1);
            // Assert
            CollectionAssert.AreEqual(courseCollection.ElementAt(0).StudentCourses,expectedStudentCourse);
        }

        [Test]
        public void AddStudentToCourse_CourseEntityNotFoundException_Test()
        {
            // Arrange
            var courseCollection = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            var studentCollection = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name ="Test"
                }
            };

            Repository.Setup(x => x.GetCourseWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseCollection.Find(c => c.Id == id));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentCollection.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.AddStudentToCourse(5, 1));
        }

        [Test]
        public void AddStudentToCourse_StudentEntityNotFoundException_Test()
        {
            // Arrange
            var courseCollection = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            var studentCollection = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name ="Test"
                }
            };

            Repository.Setup(x => x.GetCourseWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseCollection.Find(c => c.Id == id));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentCollection.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.AddStudentToCourse(1, 5));
        }

        [Test]
        public void AddStudentToCourse_EntityAlreadyExistException_Test()
        {
            // Arrange
            var courseCollection = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name ="Test",
                    StudentCourses = new List<StudentCourse>
                    {
                        new StudentCourse
                        {
                            StudentId = 1,
                            CourseId = 1
                        }
                    }
                }
            };
            var studentCollection = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name ="Test"
                }
            };

            Repository.Setup(x => x.GetCourseWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => courseCollection.Find(c => c.Id == id));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentCollection.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityAlreadyExistException>(async () => await Service.AddStudentToCourse(1, 1));
        }

        
    }
}