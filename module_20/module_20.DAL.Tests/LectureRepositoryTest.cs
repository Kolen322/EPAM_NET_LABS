using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using module_20.DAL.Repositories;
using module_20.DAL.EntityFramework;
using System.Threading.Tasks;
using module_20.DAL.Entities;
using System.Collections.Generic;

namespace module_20.DAL.Tests
{
    class LectureRepositoryTest
    {
        [Test]
        public void GetAll_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var lectures = repository.GetAll();
                Assert.That(lectures.Count(), Is.EqualTo(context.Lectures.Count()));
            }
        }

        [Test]
        public void GetAllWithCourse_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var lectures = repository.GetAllWithCourse();
                Assert.That(lectures.Count(), Is.EqualTo(context.Lectures.Count()));
                Assert.That(lectures.FirstOrDefault().Course != null);
            }
        }

        [Test]
        public void GetAllWithHomeworks_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var lectures = repository.GetAllWithHomeworks();
                Assert.That(lectures.Count(), Is.EqualTo(context.Lectures.Count()));
                Assert.That(lectures.FirstOrDefault().Homeworks != null);
            }
        }

        [Test]
        public void GetAllWithStudents_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var lectures = repository.GetAllWithStudents();
                Assert.That(lectures.Count(), Is.EqualTo(context.Lectures.Count()));
                Assert.That(lectures.FirstOrDefault().StudentLectures != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetByIdAsync_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var lecture = await repository.GetByIdAsync(id);
                Assert.That(lecture.Id, Is.EqualTo(id));
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetLectureWithCourseByIdAsync_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var lecture = await repository.GetLectureWithCourseById(id);
                Assert.That(lecture.Id, Is.EqualTo(id));
                Assert.That(lecture.Course != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetLectureWithHomeworksByIdAsync_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var lecture = await repository.GetLectureWithHomeworksById(id);
                Assert.That(lecture.Id, Is.EqualTo(id));
                Assert.That(lecture.Homeworks != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetLectureWithStudentsByIdAsync_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var lecture = await repository.GetLectureWithStudentsById(id);
                Assert.That(lecture.Id, Is.EqualTo(id));
                Assert.That(lecture.StudentLectures != null);
            }
        }

        [Test]
        public async Task AddAsync_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "AddTestDatabase")
              .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var newLecture = new Lecture { Name = "Test" };
                var prevCount = repository.GetAll().Count();
                await repository.AddAsync(newLecture);
                await context.SaveChangesAsync();
                var lectures = repository.GetAll();
                Assert.That(lectures.Count(), Is.EqualTo(prevCount + 1));
            }
        }

        [Test]
        public async Task AddRangeAsync_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "AddTestDatabase")
              .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var newLectures = new List<Lecture>
                {
                    new Lecture
                    {
                        Name = "Test"
                    },
                    new Lecture
                    {
                        Name = "Test"
                    }
                };
                var prevCount = repository.GetAll().Count();
                await repository.AddRangeAsync(newLectures);
                await context.SaveChangesAsync();
                var lectures = repository.GetAll();
                Assert.That(lectures.Count(), Is.EqualTo(prevCount + 2));
            }
        }

        [Test]
        public async Task Remove_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "RemoveTestDatabase")
              .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var removeLecture = await repository.GetByIdAsync(1);
                var prevCount = repository.GetAll().Count();

                repository.Remove(removeLecture);

                await context.SaveChangesAsync();
                var lectures = repository.GetAll();
                Assert.That(lectures.Count(), Is.EqualTo(prevCount - 1));
            }
        }

        [Test]
        public async Task RemoveRange_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "RemoveTestDatabase")
              .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var removeLectures = new List<Lecture>();
                removeLectures.AddRange(repository.GetAll().ToList());

                repository.RemoveRange(removeLectures);
                await context.SaveChangesAsync();
                var lectures = repository.GetAll();
                Assert.That(lectures.Count(), Is.EqualTo(0));
            }
        }

        [Test]
        public async Task Update_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "UpdateTestDatabase")
              .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var updateLecture = await repository.GetByIdAsync(1);
                updateLecture.Name = "Update";

                repository.Update(updateLecture);
                var result = await repository.GetByIdAsync(1);
                await context.SaveChangesAsync();
                Assert.That(result.Name, Is.EqualTo("Update"));
            }
        }

        [Test]
        public async Task Contains_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "FindTestDatabase")
              .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var newLecture = new Lecture { Name = "TESTSTSTST" };
                var contains = await repository.Contains(newLecture);
                Assert.That(contains, Is.EqualTo(false));
            }
        }

        [Test]
        public void Where_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "FindTestDatabase")
              .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var where = repository.Where(c => c.Id == 1);
                Assert.That(where.Count(), Is.EqualTo(1));
            }
        }

        [Test]
        public async Task SingleOrDefault_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
              .UseInMemoryDatabase(databaseName: "FindTestDatabase")
              .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LectureRepository(context);
                var single = await repository.SingleOrDefaultAsync(c => c.Id == 1);
                Assert.That(single.Id, Is.EqualTo(1));
            }
        }
    }
}
