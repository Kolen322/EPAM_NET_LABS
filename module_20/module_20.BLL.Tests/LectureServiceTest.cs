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
    class LectureServiceTest
    {
        private Mock<ILectureRepository> Repository { get; set; }
        private Mock<IStudentRepository> StudentRepository { get; set; }
        private Mock<ICourseRepository> CourseRepository { get; set; }
        private Mock<IHomeworkRepository> HomeworkRepository { get; set; }
        private Mock<ILecturerRepository> LecturerRepository { get; set; }
        private Mock<IUnitOfWork> UnitOfWork { get; set; }
        private ILectureService Service { get; set; }
        private Mock<ILogger<LectureService>> Logger { get; set; }

        public List<Lecture> _testLectures = new List<Lecture>
        {
            new Lecture
            {
                Id = 1,
                Name = "Test"
            },
            new Lecture
            {
                Id = 2,
                Name = "Test"
            }
        };

        [SetUp]
        public void Setup()
        {
            Repository = new Mock<ILectureRepository>();
            CourseRepository = new Mock<ICourseRepository>();
            StudentRepository = new Mock<IStudentRepository>();
            HomeworkRepository = new Mock<IHomeworkRepository>();
            LecturerRepository = new Mock<ILecturerRepository>();

            UnitOfWork = new Mock<IUnitOfWork>();

            Logger = new Mock<ILogger<LectureService>>();

            UnitOfWork.SetupGet(x => x.Lectures)
                .Returns(Repository.Object);

            UnitOfWork.SetupGet(x => x.Students)
                .Returns(StudentRepository.Object);

            UnitOfWork.SetupGet(x => x.Courses)
                .Returns(CourseRepository.Object);

            UnitOfWork.SetupGet(x => x.Homeworks)
                .Returns(HomeworkRepository.Object);

            UnitOfWork.SetupGet(x => x.Lecturers)
                .Returns(LecturerRepository.Object);

            Service = new LectureService(UnitOfWork.Object, Logger.Object);
        }

        [Test]
        public void GetAll_Test()
        {
            // Arrange
            Repository.Setup(x => x.GetAll())
                .Returns(_testLectures.AsQueryable());
            // Act
            var lectures = Service.GetAllLectures();
            // Assert
            Repository.Verify(x => x.GetAll(), Times.Once);
            Assert.That(lectures.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllLecturesWithCourse_Test()
        {
            // Arrange
            var lecturesWithCourse = new List<Lecture>(_testLectures);
            var course = new Course { Id = 1, Name = "Test" };
            lecturesWithCourse.ElementAt(0).Course = course;

            Repository.Setup(x => x.GetAllWithCourse())
                .Returns(lecturesWithCourse.AsQueryable());
            // Act
            var lectures = Service.GetAllLecturesWithCourse();
            // Assert
            Repository.Verify(x => x.GetAllWithCourse(), Times.Once);
            Assert.That(lectures.Count() == 2 && lectures.ElementAt(0).Course != null);
        }

        [Test]
        public void GetAllLecturesWithHomeworks_Test()
        {
            // Arrange
            var lecturesWithHomeworks = new List<Lecture>(_testLectures);
            var homeworks = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Task = "Test"
                }
            };
            lecturesWithHomeworks.ElementAt(0).Homeworks = homeworks;

            Repository.Setup(x => x.GetAllWithHomeworks())
                .Returns(lecturesWithHomeworks.AsQueryable());
            // Act
            var lectures = Service.GetAllLecturesWithHomeworks();
            // Assert
            Repository.Verify(x => x.GetAllWithHomeworks(), Times.Once);
            Assert.That(lectures.Count() == 2 && lectures.ElementAt(0).Homeworks != null);
        }

        [Test]
        public void GetAllLecturesWithStudents_Test()
        {
            // Arrange
            var lecturesWithStudents = new List<Lecture>(_testLectures);
            var students = new List<StudentLecture>
            {
                new StudentLecture
                {
                    StudentId = 1,
                    LectureId = 1
                }
            };
            lecturesWithStudents.ElementAt(0).StudentLectures = students;

            Repository.Setup(x => x.GetAllWithStudents())
                .Returns(lecturesWithStudents.AsQueryable());
            // Act
            var lectures = Service.GetAllLecturesWithStudents();
            // Assert
            Repository.Verify(x => x.GetAllWithStudents(), Times.Once);
            Assert.That(lectures.Count() == 2 && lectures.ElementAt(0).StudentLectures != null);
        }

        [Test]
        [TestCase(1)]
        public async Task GetLectureById_NormalConditions_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testLectures.Find(c => c.Id == id));
            // Act
            var lecture = await Service.GetLectureById(id);
            // Assert
            Repository.Verify(x => x.GetByIdAsync(id), Times.Once);
            Assert.That(lecture.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(4)]
        public void GetLectureById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _testLectures.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetLectureById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetLectureWithCourseById_NormalConditions_Test(int id)
        {
            // Arrange
            var lecturesWithCourse = new List<Lecture>(_testLectures);
            var course = new Course { Id = 1, Name = "Test" };
            lecturesWithCourse.ElementAt(0).Course = course;

            Repository.Setup(x => x.GetLectureWithCourseById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturesWithCourse.Find(c => c.Id == id));
            // Act
            var lecture = await Service.GetLectureWithCourseById(id);
            // Assert
            Repository.Verify(x => x.GetLectureWithCourseById(id), Times.Once);
            Assert.That(lecture.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(4)]
        public void GetLectureWithCourseById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var lecturesWithCourse = new List<Lecture>(_testLectures);
            var course = new Course { Id = 1, Name = "Test" };
            lecturesWithCourse.ElementAt(0).Course = course;

            Repository.Setup(x => x.GetLectureWithCourseById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturesWithCourse.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetLectureWithCourseById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetLectureWithHomeworksById_NormalConditions_Test(int id)
        {
            // Arrange
            var lecturesWithHomeworks = new List<Lecture>(_testLectures);
            var homeworks = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Task = "Test"
                }
            };
            lecturesWithHomeworks.ElementAt(0).Homeworks = homeworks;

            Repository.Setup(x => x.GetLectureWithHomeworksById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturesWithHomeworks.Find(c => c.Id == id));
            // Act
            var lecture = await Service.GetLectureWithHomeworksById(id);
            // Assert
            Repository.Verify(x => x.GetLectureWithHomeworksById(id), Times.Once);
            Assert.That(lecture.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(4)]
        public void GetLectureWithHomeworksById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var lecturesWithHomeworks = new List<Lecture>(_testLectures);
            var homeworks = new List<Homework>
            {
                new Homework
                {
                    Id = 1,
                    Task = "Test"
                }
            };
            lecturesWithHomeworks.ElementAt(0).Homeworks = homeworks;

            Repository.Setup(x => x.GetLectureWithHomeworksById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturesWithHomeworks.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetLectureWithHomeworksById(id));
        }

        [Test]
        [TestCase(1)]
        public async Task GetLectureWithStudentsById_NormalConditions_Test(int id)
        {
            // Arrange
            var lecturesWithStudents = new List<Lecture>(_testLectures);
            var students = new List<StudentLecture>
            {
                new StudentLecture
                {
                    StudentId = 1,
                    LectureId = 1
                }
            };
            lecturesWithStudents.ElementAt(0).StudentLectures = students;

            Repository.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturesWithStudents.Find(c => c.Id == id));
            // Act
            var lecture = await Service.GetLectureWithStudentsById(id);
            // Assert
            Repository.Verify(x => x.GetLectureWithStudentsById(id), Times.Once);
            Assert.That(lecture.Id, Is.EqualTo(id));
        }

        [Test]
        [TestCase(4)]
        public void GetLectureWithStudentsById_EntityNotFoundException_Test(int id)
        {
            // Arrange
            var lecturesWithStudents = new List<Lecture>(_testLectures);
            var students = new List<StudentLecture>
            {
                new StudentLecture
                {
                    StudentId = 1,
                    LectureId = 1
                }
            };
            lecturesWithStudents.ElementAt(0).StudentLectures = students;

            Repository.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturesWithStudents.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetLectureWithStudentsById(id));
        }

        [Test]
        public void CreateLecture_NormalConditions_Test()
        {
            // Arrange
            var testCreate = new List<Lecture>(_testLectures);
            var lecture = new Lecture { Id = 3, CourseId = 1, Name = "Test" };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.AddAsync(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => testCreate.Add(lecture));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(c => c.Id == id));
            CourseRepository.Setup(x => x.GetCoursesWithStudents())
                .Returns(courses.AsQueryable());
            // Act
            Service.CreateLecture(lecture);
            // Assert
            Repository.Verify(x => x.AddAsync(It.IsAny<Lecture>()), Times.Once);
            Assert.That(3, Is.EqualTo(testCreate.Count()));
        }

        [Test]
        public void CreateLecture_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.CreateLecture(null));
        }

        [Test]
        public void CreateLecture_CourseEntityNotFoundException_Test()
        {
            // Arrange
            var testCreate = new List<Lecture>(_testLectures);
            var lecture = new Lecture { Id = 3, CourseId = 2, Name = "Test" };
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.AddAsync(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => testCreate.Add(lecture));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(c => c.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.CreateLecture(lecture));
        }

        [Test]
        public void CreateLecture_EntityAlreadyExistException_Test()
        {
            // Arrange
            var testCreate = new List<Lecture>(_testLectures);
            var lecture = testCreate.ElementAt(0);
            lecture.CourseId = 1;
            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Name = "Test"
                }
            };

            Repository.Setup(x => x.AddAsync(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => testCreate.Add(lecture));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(c => c.Id == id));

            Repository.Setup(x => x.Contains(It.IsAny<Lecture>()))
               .ReturnsAsync((Lecture lecture) => testCreate.Contains(lecture));
            // Assert
            Assert.ThrowsAsync<EntityAlreadyExistException>(async () => await Service.CreateLecture(lecture));
        }

        [Test]
        public void DeleteLecture_NormalConditions_Test()
        {
            // Arrange
            var testRemove = new List<Lecture>(_testLectures);

            Repository.Setup(x => x.Remove(It.IsAny<Lecture>()))
               .Callback((Lecture lecture) => testRemove.RemoveAt(testRemove.FindIndex(x => x.Id == lecture.Id)));
            Repository.Setup(x => x.Contains(It.IsAny<Lecture>()))
                .ReturnsAsync((Lecture lecture) => testRemove.Contains(lecture));
            // Act
            var lecture = testRemove.ElementAt(0);

            Service.DeleteLecture(lecture);
            // Assert
            Repository.Verify(x => x.Remove(It.IsAny<Lecture>()), Times.Once);
            Assert.That(1, Is.EqualTo(testRemove.Count()));
        }

        [Test]
        public void DeleteLecture_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.DeleteLecture(null));
        }

        [Test]
        public void DeleteLecture_EntityNotFoundException_Test()
        {
            // Assert
            var lecture = new Lecture { Id = 10, Name = "Test" };
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.DeleteLecture(lecture));
        }

        [Test]
        public void UpdateLecture_NormalConditions_Test()
        {
            // Arrange
            var testUpdate = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test",
                    CourseId = 1
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


            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(c => c.Id == id));
            Repository.Setup(x => x.Update(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => testUpdate[testUpdate.FindIndex(x => x.Id == lecture.Id)] = lecture);

            var updateLecture = new Lecture { Id = 1, Name = "update", CourseId = 1 };

            // Act
            Service.UpdateLecture(updateLecture);
            // Assert
            Repository.Verify(x => x.Update(It.IsAny<Lecture>()), Times.Once);
            Assert.That("update", Is.EqualTo(testUpdate.ElementAt(0).Name));
        }

        [Test]
        public void UpdateLecture_EntityNullException_Test()
        {
            // Assert
            Assert.ThrowsAsync<EntityNullException>(async () => await Service.UpdateLecture(null));
        }

        [Test]
        public void UpdateLecture_EntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test",
                    CourseId = 1
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


            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(c => c.Id == id));
            Repository.Setup(x => x.Update(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => testUpdate[testUpdate.FindIndex(x => x.Id == lecture.Id)] = lecture);

            var updateLecture = new Lecture { Id = 5, Name = "update", CourseId = 1 };

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateLecture(updateLecture));
        }

        [Test]
        public void UpdateLecture_CourseEntityNotFoundException_Test()
        {
            // Arrange
            var testUpdate = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name = "Test",
                    CourseId = 1
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


            Repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => testUpdate.Find(c => c.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(c => c.Id == id));
            Repository.Setup(x => x.Update(It.IsAny<Lecture>()))
                .Callback((Lecture lecture) => testUpdate[testUpdate.FindIndex(x => x.Id == lecture.Id)] = lecture);

            var updateLecture = new Lecture { Id = 1, Name = "update", CourseId = 5 };

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.UpdateLecture(updateLecture));
        }

        [Test]
        [TestCase(1, "Test")]
        public async Task AddHomeworkToLecture_NormalConditions_Test(int lectureId, string task)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    Student = students.ElementAt(0),
                    StudentId = 1
                }
            };
            var homeworks = new List<Homework>();
            lectures.ElementAt(0).StudentLectures = studentLectures;

            Repository.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            HomeworkRepository.Setup(x => x.AddAsync(It.IsAny<Homework>()))
                .Callback((Homework homework) => homeworks.Add(homework));
            // Act
            await Service.AddHomeworkToLecture(lectureId, task);
            // Assert
            HomeworkRepository.Verify(x => x.AddAsync(It.IsAny<Homework>()), Times.Exactly(lectures.ElementAt(0).StudentLectures.Count()));
            Assert.That(1, Is.EqualTo(homeworks.Count()));
        }

        [Test]
        [TestCase(3, "Test")]
        public void AddHomeworkToLecture_EntityNotFoundException_Test(int lectureId, string task)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
                }
            };
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    Student = students.ElementAt(0),
                    StudentId = 1
                }
            };
            var homeworks = new List<Homework>();
            lectures.ElementAt(0).StudentLectures = studentLectures;

            Repository.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            HomeworkRepository.Setup(x => x.AddAsync(It.IsAny<Homework>()))
                .Callback((Homework homework) => homeworks.Add(homework));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.AddHomeworkToLecture(lectureId, task));
        }

        [Test]
        [TestCase(1)]
        public async Task GetLecturerOfLecture_NormalConditions_Test(int lectureId)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var lecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            var course = new Course { Id = 1, LecturerId = 1, Lecturer = lecturers.ElementAt(0) };
            lectures.ElementAt(0).Course = course;

            Repository.Setup(x => x.GetLectureWithCourseById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturers.Find(l => l.Id == id));
            // Act
            var lecturer = await Service.GetLecturerOfLecture(lectureId);
            // Assert
            Repository.Verify(x => x.GetLectureWithCourseById(It.IsAny<int>()), Times.Once);
            LecturerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            Assert.That(lecturer.Name, Is.EqualTo(lecturers.ElementAt(0).Name));
        }

        [Test]
        [TestCase(3)]
        public void GetLecturerOfLecture_EntityNotFoundException_Test(int lectureId)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var lecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            var course = new Course { Id = 1, LecturerId = 1, Lecturer = lecturers.ElementAt(0) };
            lectures.ElementAt(0).Course = course;

            Repository.Setup(x => x.GetLectureWithCourseById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturers.Find(l => l.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetLecturerOfLecture(lectureId));
        }

        [Test]
        [TestCase(3)]
        public void GetLecturerOfLecture_LecturerEntityNotFoundException_Test(int lectureId)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var lecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    Id = 1,
                    Name ="Test"
                }
            };
            var course = new Course { Id = 1, LecturerId = 2 };
            lectures.ElementAt(0).Course = course;

            Repository.Setup(x => x.GetLectureWithCourseById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            LecturerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => lecturers.Find(l => l.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetLecturerOfLecture(lectureId));
        }

        [Test]
        [TestCase(1,1)]
        public async Task GetNumberOfStudentLecturesMissed_NormalConditions_Test(int studentId, int courseId)
        {
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
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
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name ="Test",
                    CourseId = 1
                }
            };
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1,
                    Lecture = lectures.ElementAt(0),
                    Attendance = false
                }
            };
            students.ElementAt(0).StudentLectures = studentLectures;

            StudentRepository.Setup(x => x.GetWithLecturesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(l => l.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(l => l.Id == id));
            // Act
            var count = await Service.GetNumberOfStudentLecturesMissed(studentId, courseId);
            // Assert
            Assert.That(1, Is.EqualTo(count));
        }

        [Test]
        [TestCase(2,1)]
        public void GetNumberOfStudentLecturesMissed_StudentEntityNotFoundException_Test(int studentId, int courseId)
        {
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
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
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name ="Test",
                    CourseId = 1
                }
            };
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1,
                    Lecture = lectures.ElementAt(0),
                    Attendance = false
                }
            };
            students.ElementAt(0).StudentLectures = studentLectures;

            StudentRepository.Setup(x => x.GetWithLecturesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(l => l.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(l => l.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetNumberOfStudentLecturesMissed(studentId, courseId));
        }

        [Test]
        [TestCase(1, 2)]
        public void GetNumberOfStudentLecturesMissed_CourseEntityNotFoundException_Test(int studentId, int courseId)
        {
            var students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Test"
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
            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                    Name ="Test",
                    CourseId = 1
                }
            };
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1,
                    Lecture = lectures.ElementAt(0),
                    Attendance = false
                }
            };
            students.ElementAt(0).StudentLectures = studentLectures;

            StudentRepository.Setup(x => x.GetWithLecturesByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => students.Find(l => l.Id == id));
            CourseRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => courses.Find(l => l.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.GetNumberOfStudentLecturesMissed(studentId, courseId));
        }

        [Test]
        [TestCase(1)]
        public async Task MarkAbsense_NormalConditions_Test(int lectureId)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1,
                    Attendance = true
                }
            };
            lectures.ElementAt(0).StudentLectures = studentLectures;
            var studentIds = new List<int> { 1 };
            Repository.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            // Act
            await Service.MarkAbsence(lectureId, studentIds);
            // Assert
            Repository.Verify(x => x.GetLectureWithStudentsById(It.IsAny<int>()), Times.Once);
            Assert.That(false, Is.EqualTo(studentLectures.ElementAt(0).Attendance));
        }

        [Test]
        [TestCase(3)]
        public void MarkAbsense_EntityNotFoundException_Test(int lectureId)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1,
                    Attendance = true
                }
            };
            lectures.ElementAt(0).StudentLectures = studentLectures;
            var studentIds = new List<int> { 1 };
            Repository.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.MarkAbsence(lectureId, studentIds));
        }

        [Test]
        [TestCase(1)]
        public async Task MarkAttendance_NormalConditions_Test(int lectureId)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1,
                    Attendance = false
                }
            };
            lectures.ElementAt(0).StudentLectures = studentLectures;
            var studentIds = new List<int> { 1 };
            Repository.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            // Act
            await Service.MarkAttendance(lectureId, studentIds);
            // Assert
            Repository.Verify(x => x.GetLectureWithStudentsById(It.IsAny<int>()), Times.Once);
            Assert.That(true, Is.EqualTo(studentLectures.ElementAt(0).Attendance));
        }

        [Test]
        [TestCase(3)]
        public void MarkAttendance_EntityNotFoundException_Test(int lectureId)
        {
            // Arrange
            var lectures = new List<Lecture>(_testLectures);
            var studentLectures = new List<StudentLecture>
            {
                new StudentLecture
                {
                    LectureId = 1,
                    StudentId = 1,
                    Attendance = true
                }
            };
            lectures.ElementAt(0).StudentLectures = studentLectures;
            var studentIds = new List<int> { 1 };
            Repository.Setup(x => x.GetLectureWithStudentsById(It.IsAny<int>()))
                .ReturnsAsync((int id) => lectures.Find(l => l.Id == id));
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await Service.MarkAttendance(lectureId, studentIds));
        }
    }
}
