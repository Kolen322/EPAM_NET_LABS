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

    class StudentServiceTest
    {
        private Mock<IStudentRepository> Repository { get; set; }
        private Mock<IUnitOfWork> UnitOfWork { get; set; }
        private IStudentService Service { get; set; }
        private Mock<ILogger<StudentService>> Logger { get; set; }

        private List<Student> _testStudents = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name ="Test"
            },
            new Student
            {
                Id = 2,
                Name = "Test"
            }
        };

        [SetUp]
        public void Setup()
        {
            Repository = new Mock<IStudentRepository>();

            UnitOfWork = new Mock<IUnitOfWork>();

            Logger = new Mock<ILogger<StudentService>>();

            UnitOfWork.SetupGet(x => x.Students)
                .Returns(Repository.Object);

            Service = new StudentService(UnitOfWork.Object, Logger.Object);
        }

        [Test]
        public void GetAll_Test()
        {
            // Arrange
            Repository.Setup(x => x.GetAll())
                .Returns(_testStudents.AsQueryable());
            // Act
            var students = Service.GetAllStudents();
            // Assert
            Repository.Verify(x => x.GetAll(), Times.Once);
            Assert.That(students.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllStudentsWithCourses_Test()
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentCourses = studentCourses;
            
            Repository.Setup(x => x.GetAllWithCourses())
                .Returns(studentsTest.AsQueryable());
            // Act
            var students = Service.GetAllStudentsWithCourses();
            // Assert
            Repository.Verify(x => x.GetAllWithCourses(), Times.Once);
            Assert.That(students.Count(), Is.EqualTo(2));
            Assert.That(studentsTest.ElementAt(0).StudentCourses != null);
        }

        [Test]
        public void GetAllStudentsWithLectures_Test()
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentLectures = studentLectures;

            Repository.Setup(x => x.GetAllWithLectures())
                .Returns(studentsTest.AsQueryable());
            // Act
            var students = Service.GetAllStudentsWithLectures();
            // Assert
            Repository.Verify(x => x.GetAllWithLectures(), Times.Once);
            Assert.That(students.Count(), Is.EqualTo(2));
            Assert.That(studentsTest.ElementAt(0).StudentLectures != null);
        }

        [Test]
        public void GetAllStudentsWithLecturesAndCourses_Test()
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentLectures = studentLectures;
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentCourses = studentCourses;
            Repository.Setup(x => x.GetAllWithLecturesAndCourses())
                .Returns(studentsTest.AsQueryable());
            // Act
            var students = Service.GetAllStudentsWithLecturesAndCourses();
            // Assert
            Repository.Verify(x => x.GetAllWithLecturesAndCourses(), Times.Once);
            Assert.That(students.Count(), Is.EqualTo(2));
            Assert.That(studentsTest.ElementAt(0).StudentLectures != null);
            Assert.That(studentsTest.ElementAt(0).StudentCourses != null);
        }

        [Test]
        [TestCase(1)]
        public async Task GetStudentById_NormalConditions_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testStudents.Find(c => c.Id == id));
            // Act
            var student = await Service.GetStudentById(id);
            // Assert
            Repository.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.That(student.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(4)]
        public void GetStudentById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testStudents.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetStudentById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetStudentWithCoursesById_NormalConditions_Test(int id)
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentCourses = studentCourses;
            Repository.Setup(x => x.GetWithCoursesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentsTest.Find(s => s.Id == id));
            // Act
            var student = await Service.GetStudentWithCoursesById(id);
            // Assert
            Repository.Verify(x => x.GetWithCoursesByIdAsync(id), Times.Once);
            Assert.That(student.Id == id && studentsTest.ElementAt(0).StudentCourses != null);
        }

        [Test]
        [TestCase(4)]
        public void GetStudentWithCoursesById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentCourses = studentCourses;
            Repository.Setup(x => x.GetWithCoursesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentsTest.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetStudentWithCoursesById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetStudentWithLecturesById_NormalConditions_Test(int id)
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentLectures = studentLectures;
            Repository.Setup(x => x.GetWithLecturesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentsTest.Find(s => s.Id == id));
            // Act
            var student = await Service.GetStudentWithLecturesById(id);
            // Assert
            Repository.Verify(x => x.GetWithLecturesByIdAsync(id), Times.Once);
            Assert.That(student.Id == id && studentsTest.ElementAt(0).StudentLectures != null);
        }

        [Test]
        [TestCase(4)]
        public void GetStudentWithLecturesById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentLectures = studentLectures;
            Repository.Setup(x => x.GetWithLecturesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentsTest.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetStudentWithCoursesById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetStudentWithLecturesAndCoursesById_NormalConditions_Test(int id)
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1
                }
            };
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentCourses = studentCourses;
            studentsTest.ElementAt(0).StudentLectures = studentLectures;
            Repository.Setup(x => x.GetWithLecturesAndCoursesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentsTest.Find(s => s.Id == id));
            // Act
            var student = await Service.GetStudentWithLecturesAndCoursesById(id);
            // Assert
            Repository.Verify(x => x.GetWithLecturesAndCoursesByIdAsync(id), Times.Once);
            Assert.That(student.Id == id && studentsTest.ElementAt(0).StudentLectures != null && studentsTest.ElementAt(0).StudentCourses != null);
        }

        [Test]
        [TestCase(4)]
        public void GetStudentWithLecturesAndCoursesById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var studentsTest = new List<Student>(_testStudents);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1
                }
            };
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse
                {
                    CourseId = 1,
                    StudentId = 1
                }
            };
            studentsTest.ElementAt(0).StudentCourses = studentCourses;
            studentsTest.ElementAt(0).StudentLectures = studentLectures;
            Repository.Setup(x => x.GetWithLecturesAndCoursesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => studentsTest.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetStudentWithLecturesAndCoursesById(id));
        }

        [Test]
        public void CreateStudent_NormalConditions_Test()
        {
            // Arrange
            var testCreate = new List<Student>(_testStudents);
            var student = new Student { Id = 3, Name = "Test" };

            Repository.Setup(x => x.AddAsync(It.IsAny<Student>()))
                .Callback((Student student) => testCreate.Add(student));
            // Act
            Service.CreateStudent(student);
            // Assert
            Repository.Verify(x => x.AddAsync(It.IsAny<Student>()), Times.Once);
            Assert.That(3, Is.EqualTo(testCreate.Count()));
        }

        [Test]
        public void CreateStudent_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.CreateStudent(null));
        }

        [Test]
        public void CreateStudent_EntityAlreadyExistException_Test()
        {
            // Arrange
            var testCreate = new List<Student>(_testStudents);
            var student = testCreate.ElementAt(0);

            Repository.Setup(x => x.AddAsync(It.IsAny<Student>()))
                .Callback((Student student) => testCreate.Add(student));
            Repository.Setup(x => x.Contains(It.IsAny<Student>()))
               .ReturnsAsync((Student student) => testCreate.Contains(student));
            // Assert
            Assert.ThrowsAsync<EntityAlreadyExistException>(async () => await Service.CreateStudent(student));
        }

        [Test]
        public void DeleteStudent_NormalConditions_Test()
        {
            // Arrange
            var testRemove = new List<Student>(_testStudents);

            Repository.Setup(x => x.Remove(It.IsAny<Student>()))
                .Callback((Student student) => testRemove.RemoveAt(testRemove.FindIndex(x => x.Id == student.Id)));
            Repository.Setup(x => x.Contains(It.IsAny<Student>()))
                .ReturnsAsync((Student student) => testRemove.Contains(student));
            // Act
            var student = testRemove.ElementAt(0);
            Service.DeleteStudent(student);
            // Assert
            Repository.Verify(x => x.Remove(It.IsAny<Student>()), Times.Once);
            Assert.That(1, Is.EqualTo(testRemove.Count()));
        }

        [Test]
        public void DeleteStudent_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.DeleteStudent(null));
        }

        [Test]
        public void DeleteStudent_EntityNotFoundException_Test()
        {
            // Assert
            var student = new Student { Id = 10, Name = "Test" };
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.DeleteStudent(student));
        }

        [Test]
        public void UpdateStudent_NormalConditions_Test()
        {
            // Arrange
            var testUpdate = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Student>()))
               .Callback((Student student) => testUpdate[testUpdate.FindIndex(x => x.Id == student.Id)] = student);
            var updateStudent = new Student { Id = 1, Name = "update" };
            // Act
            Service.UpdateStudent(updateStudent);
            // Assert
            Repository.Verify(x => x.Update(It.IsAny<Student>()), Times.Once);
            Assert.That("update", Is.EqualTo(testUpdate.ElementAt(0).Name));
        }

        [Test]
        public void UpdateStudent_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.UpdateStudent(null));
        }

        [Test]
        public void UpdateStudent_EntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            // Assert0
            var student = new Student { Id = 10, Name = "Test" };
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateStudent(student));
        }
    }
}
