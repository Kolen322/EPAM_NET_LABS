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
using MockQueryable.Moq;
using Microsoft.Extensions.Logging;

namespace module_20.BLL.Tests
{
    public class HomeworkServiceTest
    {
        private List<Homework> _testHomeworks = new List<Homework>
        {
            new Homework
            {
                Id = 1,
                Mark = 5,
                Task = "Test"
            },
            new Homework
            {
                Id = 2,
                Mark = 5,
                Task = "Test"
            }
        };
        private Mock<IHomeworkRepository> Repository { get; set; }
        private Mock<ICourseRepository> CourseRepository { get; set; }
        private Mock<IStudentRepository> StudentRepository { get; set; }
        private Mock<ILectureRepository> LectureRepository { get; set; }
        private Mock<IUnitOfWork> UnitOfWork { get; set; }
        private IHomeworkService Service { get; set; }
        private Mock<ILogger<HomeworkService>> Logger { get; set; }

        [SetUp]
        public void Setup()
        {
            Repository = new Mock<IHomeworkRepository>();
            CourseRepository = new Mock<ICourseRepository>();
            StudentRepository = new Mock<IStudentRepository>();
            LectureRepository = new Mock<ILectureRepository>();

            UnitOfWork = new Mock<IUnitOfWork>();

            Logger = new Mock<ILogger<HomeworkService>>();

            UnitOfWork.SetupGet(x => x.Homeworks)
               .Returns(Repository.Object);

            UnitOfWork.SetupGet(x => x.Lectures)
                .Returns(LectureRepository.Object);

            UnitOfWork.SetupGet(x => x.Students)
                .Returns(StudentRepository.Object);

            UnitOfWork.SetupGet(x => x.Courses)
                .Returns(CourseRepository.Object);

            Service = new HomeworkService(UnitOfWork.Object, Logger.Object);
        }

        [Test]
        public void GetAll_Test()
        {
            // Arrange
            Repository.Setup(x => x.GetAll())
                .Returns(_testHomeworks.AsQueryable());
            // Act
            var homeworks = Service.GetAllHomeworks();
            // Assert
            Repository.Verify(x => x.GetAll(), Times.Once);
            Assert.That(homeworks.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllWithLecture_Test()
        {
            // Arrange
            var homeworksWithLecture = new List<Homework>(_testHomeworks);
            var lecture = new Lecture { Id = 1, Name = "Test" };
            homeworksWithLecture.ElementAt(0).Lecture = lecture;
            homeworksWithLecture.ElementAt(0).LectureId = 1;
            Repository.Setup(x => x.GetAllWithLecture())
               .Returns(homeworksWithLecture.AsQueryable());
            // Act
            var testHomework = Service.GetAllHomeworksWithLecture();
            // Assert
            Repository.Verify(x => x.GetAllWithLecture(), Times.Once);
            Assert.That(testHomework.Count() == 2 && testHomework.ElementAt(0).Lecture != null);
        }

        [Test]
        public void GetAllWithStudent_Test()
        {
            // Arrange
            var homeworksWithLecture = new List<Homework>(_testHomeworks);
            var student = new Student { Id = 1, Name = "Test" };
            homeworksWithLecture.ElementAt(0).Student = student;
            homeworksWithLecture.ElementAt(0).StudentId = 1;
            Repository.Setup(x => x.GetAllWithStudent())
               .Returns(homeworksWithLecture.AsQueryable());
            // Act
            var testHomework = Service.GetAllHomeworksWithStudent();
            // Assert
            Repository.Verify(x => x.GetAllWithStudent(), Times.Once);
            Assert.That(testHomework.Count() == 2 && testHomework.ElementAt(0).Student != null);
        }

        [Test]
        public void GetAllWithLectureAndStudent_Test()
        {
            // Arrange
            var homeworksWithLecture = new List<Homework>(_testHomeworks);
            var student = new Student { Id = 1, Name = "Test" };
            var lecture = new Lecture { Id = 1, Name = "Test" };
            homeworksWithLecture.ElementAt(0).Student = student;
            homeworksWithLecture.ElementAt(0).StudentId = 1;
            homeworksWithLecture.ElementAt(0).Lecture = lecture;
            homeworksWithLecture.ElementAt(0).LectureId = 1;
            Repository.Setup(x => x.GetAllWithLectureAndStudent())
               .Returns(homeworksWithLecture.AsQueryable());
            // Act
            var testHomework = Service.GetAllHomeworksWithLectureAndStudent();
            // Assert
            Repository.Verify(x => x.GetAllWithLectureAndStudent(), Times.Once);
            Assert.That(testHomework.Count() == 2
                && testHomework.ElementAt(0).Student != null
                && testHomework.ElementAt(0).Lecture != null);
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkById_NormalConditions_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testHomeworks.Find(c => c.Id == id));
            // Act
            var homework = await Service.GetHomeworkById(id);
            // Assert
            Repository.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.That(homework.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(4)]
        public void GetHomeworkById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testHomeworks.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetHomeworkById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkWithLectureById_NormalConditions_Test(int id)
        {
            // Arrange
            var homeworkWithLecture = new List<Homework>(_testHomeworks);
            var lecture = new Lecture { Id = 1, Name = "Test" };
            homeworkWithLecture.ElementAt(id - 1).Lecture = lecture;

            Repository.Setup(x => x.GetHomeworkWithLectureById(It.IsAny<int>()))
                .ReturnsAsync((int id) => homeworkWithLecture.Find(c => c.Id == id));
            // Act
            var homework = await Service.GetHomeworkWithLectureById(id);
            // Assert
            Repository.Verify(x => x.GetHomeworkWithLectureById(id), Times.Once);
            Assert.That(homework.Id, Is.EqualTo(id));
            Assert.That(homework.Lecture != null);
        }

        [Test]
        [TestCase(4)]
        public void GetHomeworkWithLectureById_EntityNotFound_Test(int id)
        {
            // Arrange
            var homeworkWithLecture = new List<Homework>(_testHomeworks);
            var lecture = new Lecture { Id = 1, Name = "Test" };
            homeworkWithLecture.ElementAt(0).Lecture = lecture;

            Repository.Setup(x => x.GetHomeworkWithLectureById(It.IsAny<int>()))
                .ReturnsAsync((int id) => homeworkWithLecture.Find(c => c.Id == id));
            // Act
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetHomeworkWithLectureById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkWithStudentById_NormalConditions_Test(int id)
        {
            // Arrange
            var homeworkWithStudent = new List<Homework>(_testHomeworks);
            var student = new Student { Id = 1, Name = "Test" };
            homeworkWithStudent.ElementAt(id - 1).Student = student;

            Repository.Setup(x => x.GetHomeworkWithStudentById(It.IsAny<int>()))
                .ReturnsAsync((int id) => homeworkWithStudent.Find(c => c.Id == id));
            // Act
            var homework = await Service.GetHomeworkWithStudentById(id);
            // Assert
            Repository.Verify(x => x.GetHomeworkWithStudentById(id), Times.Once);
            Assert.That(homework.Id, Is.EqualTo(id));
            Assert.That(homework.Student != null);
        }

        [Test]
        [TestCase(4)]
        public void GetHomeworkWithStudentById_EntityNotFound_Test(int id)
        {
            // Arrange
            var homeworkWithStudent = new List<Homework>(_testHomeworks);
            var student = new Student { Id = 1, Name = "Test" };
            homeworkWithStudent.ElementAt(0).Student = student;

            Repository.Setup(x => x.GetHomeworkWithStudentById(It.IsAny<int>()))
                .ReturnsAsync((int id) => homeworkWithStudent.Find(c => c.Id == id));
            // Act
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetHomeworkWithStudentById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkWithLectureAndStudentById_NormalConditions_Test(int id)
        {
            // Arrange
            var homeworkWithLectureAndStudent = new List<Homework>(_testHomeworks);
            var student = new Student { Id = 1, Name = "Test" };
            var lecture = new Lecture { Id = 1, Name = "Test" };
            homeworkWithLectureAndStudent.ElementAt(id - 1).Student = student;
            homeworkWithLectureAndStudent.ElementAt(id - 1).Lecture = lecture;

            Repository.Setup(x => x.GetHomeworkWithLectureAndStudentById(It.IsAny<int>()))
                .ReturnsAsync((int id) => homeworkWithLectureAndStudent.Find(c => c.Id == id));
            // Act
            var homework = await Service.GetHomeworkWithLectureAndStudentById(id);
            // Assert
            Repository.Verify(x => x.GetHomeworkWithLectureAndStudentById(id), Times.Once);
            Assert.That(homework.Id, Is.EqualTo(id));
            Assert.That(homework.Student != null);
            Assert.That(homework.Lecture != null);
        }

        [Test]
        [TestCase(4)]
        public void GetHomeworkWithLectureAndStudentById_EntityNotFound_Test(int id)
        {
            // Arrange
            var homeworkWithLectureAndStudent = new List<Homework>(_testHomeworks);
            var student = new Student { Id = 1, Name = "Test" };
            var lecture = new Lecture { Id = 1, Name = "Test" };
            homeworkWithLectureAndStudent.ElementAt(0).Student = student;
            homeworkWithLectureAndStudent.ElementAt(0).Lecture = lecture;

            Repository.Setup(x => x.GetHomeworkWithLectureAndStudentById(It.IsAny<int>()))
                .ReturnsAsync((int id) => homeworkWithLectureAndStudent.Find(c => c.Id == id));
            // Act
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetHomeworkWithLectureAndStudentById(id));
        }

        [Test]
        public void CreateHomework_NormalConditions_Test()
        {
            // Arrange
            var testCreate = new List<Homework>(_testHomeworks);
            var homework = new Homework { Id = 3, LectureId = 1, StudentId = 1, Task = "Test", Mark = 5 };

            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.AddAsync(It.IsAny<Homework>()))
                .Callback((Homework homework) => testCreate.Add(homework));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            LectureRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(s => s.Id == id));
            // Act
            Service.CreateHomework(homework);
            // Assert
            Repository.Verify(x => x.AddAsync(It.IsAny<Homework>()), Times.Once);
            Assert.That(3, Is.EqualTo(testCreate.Count()));
        }

        [Test]
        public void CreateHomework_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.CreateHomework(null));
        }

        [Test]
        public void CreateHomework_LectureEntityNotFoundException_Test()
        {
            // Arrange
            var testCreate = new List<Homework>(_testHomeworks);
            var homework = new Homework { Id = 3, LectureId = 2, StudentId = 1, Task = "Test", Mark = 5 };

            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.AddAsync(It.IsAny<Homework>()))
                .Callback((Homework homework) => testCreate.Add(homework));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            LectureRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.CreateHomework(homework));
        }

        [Test]
        public void CreateHomework_EntityAlreadyExistException_Test()
        {
            // Arrange
            var testCreate = new List<Homework>(_testHomeworks);
            var homework = testCreate.ElementAt(0);
            homework.LectureId = 1;
            homework.StudentId = 1;

            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.AddAsync(It.IsAny<Homework>()))
                .Callback((Homework homework) => testCreate.Add(homework));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            LectureRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(s => s.Id == id));
            Repository.Setup(x => x.Contains(It.IsAny<Homework>()))
                .ReturnsAsync((Homework homework) => testCreate.Contains(homework));
            // Assert
            Assert.ThrowsAsync<EntityAlreadyExistException>(async () => await Service.CreateHomework(homework));
        }

        [Test]
        public void CreateHomework_StudentEntityNotFoundException_Test()
        {
            // Arrange
            var testCreate = new List<Homework>(_testHomeworks);
            var homework = new Homework { Id = 3, LectureId = 1, StudentId = 2, Task = "Test", Mark = 5 };

            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.AddAsync(It.IsAny<Homework>()))
                .Callback((Homework homework) => testCreate.Add(homework));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            LectureRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.CreateHomework(homework));
        }

        [Test]
        public void DeleteHomework_NormalConditions_Test()
        {
            // Arrange
            var testRemove = new List<Homework>(_testHomeworks);

            Repository.Setup(x => x.Remove(It.IsAny<Homework>()))
                .Callback((Homework homework) => testRemove.RemoveAt(testRemove.FindIndex(x => x.Id == homework.Id)));
            Repository.Setup(x => x.Contains(It.IsAny<Homework>()))
                .ReturnsAsync((Homework homework) => testRemove.Contains(homework));
            // Act
            var homework = testRemove.ElementAt(0);

            Service.DeleteHomework(homework);
            // Assert
            Repository.Verify(x => x.Remove(It.IsAny<Homework>()), Times.Once);
            Assert.That(1, Is.EqualTo(testRemove.Count()));
        }

        [Test]
        public void DeleteHomework_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.DeleteHomework(null));
        }

        [Test]
        public void DeleteHomework_EntityNotFoundException_Test()
        {
            // Assert
            var homework = new Homework { Id = 10, Task = "Test", Mark = 5 };
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.DeleteHomework(homework));
        }

        [Test]
        public void UpdateHomework_NormalConditions_Test()
        {
            // Arrange
            var testUpdate = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 5,
                    Task = "Test",
                    LectureId = 1,
                    StudentId = 1
                }
            };
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            LectureRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(s => s.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Homework>()))
                .Callback((Homework homework) => testUpdate[testUpdate.FindIndex(x => x.Id == homework.Id)] = homework);

            var updateHomework = new Homework
            {
                Id = 1,
                Mark = 5,
                Task = "update",
                LectureId = 1,
                StudentId = 1
            };
            // Act
            Service.UpdateHomework(updateHomework);
            // Assert
            Repository.Verify(x => x.Update(It.IsAny<Homework>()), Times.Once);
            Assert.That("update", Is.EqualTo(testUpdate.ElementAt(0).Task));
        }

        [Test]
        public void UpdateHomework_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.UpdateHomework(null));
        }

        [Test]
        public void UpdateHomework_EntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 5,
                    Task = "Test",
                    LectureId = 1,
                    StudentId = 1
                }
            };
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            LectureRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(s => s.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Homework>()))
                .Callback((Homework homework) => testUpdate[testUpdate.FindIndex(x => x.Id == homework.Id)] = homework);

            var updateHomework = new Homework
            {
                Id = 5,
                Mark = 5,
                Task = "update",
                LectureId = 1,
                StudentId = 1
            };
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateHomework(updateHomework));
        }

        [Test]
        public void UpdateHomework_StudentEntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 5,
                    Task = "Test",
                    LectureId = 1,
                    StudentId = 1
                }
            };
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            LectureRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(s => s.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Homework>()))
                .Callback((Homework homework) => testUpdate[testUpdate.FindIndex(x => x.Id == homework.Id)] = homework);

            var updateHomework = new Homework
            {
                Id = 1,
                Mark = 5,
                Task = "update",
                LectureId = 1,
                StudentId = 2
            };
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateHomework(updateHomework));
        }

        [Test]
        public void UpdateHomework_LectureEntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 5,
                    Task = "Test",
                    LectureId = 1,
                    StudentId = 1
                }
            };
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            LectureRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(s => s.Id == id));

            Repository.Setup(x => x.Update(It.IsAny<Homework>()))
                .Callback((Homework homework) => testUpdate[testUpdate.FindIndex(x => x.Id == homework.Id)] = homework);

            var updateHomework = new Homework
            {
                Id = 1,
                Mark = 5,
                Task = "update",
                LectureId = 2,
                StudentId = 1
            };
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateHomework(updateHomework));
        }

        [Test]
        [TestCase(1, 1)]
        public async Task GetAverageMark_NormalConditions_Test(int studentId, int courseId)
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Lecture1"
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 1,
                    Name = "Lecture2"
                }
            };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var testHomework = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 5,
                    Task = "Test",
                    LectureId = 1,
                    Lecture = lectures.ElementAt(0),
                    StudentId = 1
                },
                new Homework
                {
                    Id = 2,
                    Mark = 1,
                    Task = "Test2",
                    LectureId = 2,
                    Lecture = lectures.ElementAt(1),
                    StudentId = 1
                }
            };
            var mockCollection = testHomework.AsQueryable().BuildMock();
            Repository.Setup(x => x.GetAllWithLecture())
                .Returns(mockCollection.Object);
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(s => s.Id == id));
            // Act
            double averageMark = await Service.GetAverageMark(studentId, courseId);
            // Assert
            Assert.That(3, Is.EqualTo(averageMark));
        }

        [Test]
        [TestCase(2, 1)]
        public void GetAverageMark_StudentEntityNotFoundException_Test(int studentId, int courseId)
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Lecture1"
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 1,
                    Name = "Lecture2"
                }
            };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var testHomework = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 5,
                    Task = "Test",
                    LectureId = 1,
                    Lecture = lectures.ElementAt(0),
                    StudentId = 1
                },
                new Homework
                {
                    Id = 2,
                    Mark = 1,
                    Task = "Test2",
                    LectureId = 2,
                    Lecture = lectures.ElementAt(1),
                    StudentId = 1
                }
            };
            var mockCollection = testHomework.AsQueryable().BuildMock();
            Repository.Setup(x => x.GetAllWithLecture())
                .Returns(mockCollection.Object);
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetAverageMark(studentId, courseId));
        }

        [Test]
        [TestCase(1, 2)]
        public void GetAverageMark_CourseEntityNotFoundException_Test(int studentId, int courseId)
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Lecture1"
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 1,
                    Name = "Lecture2"
                }
            };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var testHomework = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 5,
                    Task = "Test",
                    LectureId = 1,
                    Lecture = lectures.ElementAt(0),
                    StudentId = 1
                },
                new Homework
                {
                    Id = 2,
                    Mark = 1,
                    Task = "Test2",
                    LectureId = 2,
                    Lecture = lectures.ElementAt(1),
                    StudentId = 1
                }
            };
            var mockCollection = testHomework.AsQueryable().BuildMock();
            Repository.Setup(x => x.GetAllWithLecture())
                .Returns(mockCollection.Object);
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetAverageMark(studentId, courseId));
        }

        [Test]
        [TestCase(1, 1)]
        public async Task CheckAverageMark_NormalConditions_Test(int studentId, int courseId)
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Lecture1"
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 1,
                    Name = "Lecture2"
                }
            };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var testHomework = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 2,
                    Task = "Test",
                    LectureId = 1,
                    Lecture = lectures.ElementAt(0),
                    StudentId = 1
                },
                new Homework
                {
                    Id = 2,
                    Mark = 1,
                    Task = "Test2",
                    LectureId = 2,
                    Lecture = lectures.ElementAt(1),
                    StudentId = 1
                }
            };
            var messageSender = new Mock<IMessageSenderService>();

            var mockCollection = testHomework.AsQueryable().BuildMock();
            Repository.Setup(x => x.GetAllWithLecture())
                .Returns(mockCollection.Object);
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(s => s.Id == id));
            // Act
            await Service.CheckAverageMark(studentId, courseId, messageSender.Object);
            // Assert
            messageSender.Verify(x => x.Send(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        [TestCase(1, 2)]
        public void CheckAverageMark_CourseEntityNotFoundException_Test(int studentId, int courseId)
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Lecture1"
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 1,
                    Name = "Lecture2"
                }
            };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var testHomework = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 2,
                    Task = "Test",
                    LectureId = 1,
                    Lecture = lectures.ElementAt(0),
                    StudentId = 1
                },
                new Homework
                {
                    Id = 2,
                    Mark = 1,
                    Task = "Test2",
                    LectureId = 2,
                    Lecture = lectures.ElementAt(1),
                    StudentId = 1
                }
            };
            var messageSender = new Mock<IMessageSenderService>();

            var mockCollection = testHomework.AsQueryable().BuildMock();
            Repository.Setup(x => x.GetAllWithLecture())
                .Returns(mockCollection.Object);
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.CheckAverageMark(studentId,courseId,messageSender.Object));
        }

        [Test]
        [TestCase(2, 1)]
        public void CheckAverageMark_StudentEntityNotFoundException_Test(int studentId, int courseId)
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Lecture1"
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 1,
                    Name = "Lecture2"
                }
            };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var testHomework = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 2,
                    Task = "Test",
                    LectureId = 1,
                    Lecture = lectures.ElementAt(0),
                    StudentId = 1
                },
                new Homework
                {
                    Id = 2,
                    Mark = 1,
                    Task = "Test2",
                    LectureId = 2,
                    Lecture = lectures.ElementAt(1),
                    StudentId = 1
                }
            };
            var messageSender = new Mock<IMessageSenderService>();

            var mockCollection = testHomework.AsQueryable().BuildMock();
            Repository.Setup(x => x.GetAllWithLecture())
                .Returns(mockCollection.Object);
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.CheckAverageMark(studentId, courseId, messageSender.Object));
        }

        [Test]
        [TestCase(1, 1)]
        public void CheckAverageMark_MessageSenderServiceEntityNullExceptionTest_Test(int studentId, int courseId)
        {
            // Arrange
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Lecture1"
                },
                new Lecture
                {
                    Id = 2,
                    CourseId = 1,
                    Name = "Lecture2"
                }
            };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var testHomework = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Mark = 2,
                    Task = "Test",
                    LectureId = 1,
                    Lecture = lectures.ElementAt(0),
                    StudentId = 1
                },
                new Homework
                {
                    Id = 2,
                    Mark = 1,
                    Task = "Test2",
                    LectureId = 2,
                    Lecture = lectures.ElementAt(1),
                    StudentId = 1
                }
            };
            var messageSender = new Mock<IMessageSenderService>();

            var mockCollection = testHomework.AsQueryable().BuildMock();
            Repository.Setup(x => x.GetAllWithLecture())
                .Returns(mockCollection.Object);
            StudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(s => s.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(s => s.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.CheckAverageMark(studentId, courseId, null));
        }
    }
}
