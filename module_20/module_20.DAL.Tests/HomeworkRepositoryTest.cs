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
    class HomeworkRepositoryTest
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
                var repository = new HomeworkRepository(context);
                var homeworks = repository.GetAll();
                Assert.That(homeworks.Count(), Is.EqualTo(context.Homeworks.Count()));
            }
        }

        [Test]
        public void GetAllWithLecture_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new HomeworkRepository(context);
                var homeworks = repository.GetAllWithLecture();
                Assert.That(homeworks.Count(), Is.EqualTo(context.Homeworks.Count()));
                Assert.That(homeworks.FirstOrDefault().Lecture != null);
            }
        }

        [Test]
        public void GetAllWithLectureAndStudent_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new HomeworkRepository(context);
                var homeworks = repository.GetAllWithLectureAndStudent();
                Assert.That(homeworks.Count(), Is.EqualTo(context.Homeworks.Count()));
                Assert.That(homeworks.FirstOrDefault().Lecture != null);
                Assert.That(homeworks.FirstOrDefault().Student != null);
            }
        }

        [Test]
        public void GetAllWithStudent_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new HomeworkRepository(context);
                var homeworks = repository.GetAllWithStudent();
                Assert.That(homeworks.Count(), Is.EqualTo(context.Homeworks.Count()));
                Assert.That(homeworks.FirstOrDefault().Student != null);
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
                var repository = new HomeworkRepository(context);
                var course = await repository.GetByIdAsync(id);
                Assert.That(course.Id, Is.EqualTo(id));
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkWithLectureAndStudentById_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new HomeworkRepository(context);
                var course = await repository.GetHomeworkWithLectureAndStudentById(id);
                Assert.That(course.Id, Is.EqualTo(id));
                Assert.That(course.Lecture != null);
                Assert.That(course.Student != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkWithStudentById_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new HomeworkRepository(context);
                var course = await repository.GetHomeworkWithStudentById(id);
                Assert.That(course.Id, Is.EqualTo(id));
                Assert.That(course.Student != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetHomeworkWithLectureById_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new HomeworkRepository(context);
                var course = await repository.GetHomeworkWithLectureById(id);
                Assert.That(course.Id, Is.EqualTo(id));
                Assert.That(course.Lecture != null);
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
                var repository = new HomeworkRepository(context);
                var newHomework = new Homework { Mark = 5, Task = "Test", LectureId = 1, StudentId = 2 };
                var prevCount = repository.GetAll().Count();
                await repository.AddAsync(newHomework);
                await context.SaveChangesAsync();
                var homeworks = repository.GetAll();
                Assert.That(homeworks.Count(), Is.EqualTo(prevCount + 1));
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
                var repository = new HomeworkRepository(context);
                var newHomeworks = new List<Homework>
                {
                    new Homework
                    {
                        Mark = 0,
                        Task = "Test",
                        StudentId = 4,
                        LectureId = 2
                    },
                    new Homework
                    {
                        Mark = 5,
                        Task = "Test",
                        StudentId = 5,
                        LectureId = 2
                    }
                };
                var prevCount = repository.GetAll().Count();
                await repository.AddRangeAsync(newHomeworks);
                await context.SaveChangesAsync();
                var homeworks = repository.GetAll();
                Assert.That(homeworks.Count(), Is.EqualTo(prevCount + 2));
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
                var repository = new HomeworkRepository(context);
                var removeHomework = await repository.GetByIdAsync(1);
                var prevCount = repository.GetAll().Count();

                repository.Remove(removeHomework);

                await context.SaveChangesAsync();
                var homeworks = repository.GetAll();
                Assert.That(homeworks.Count(), Is.EqualTo(prevCount - 1));
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
                var repository = new HomeworkRepository(context);
                var removeHomeworks = new List<Homework>();
                removeHomeworks.AddRange(repository.GetAll().ToList());

                repository.RemoveRange(removeHomeworks);
                await context.SaveChangesAsync();
                var homeworks = repository.GetAll();
                Assert.That(homeworks.Count(), Is.EqualTo(0));
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
                var repository = new HomeworkRepository(context);
                var updateHomework = await repository.GetByIdAsync(1);
                updateHomework.Task = "Update";

                repository.Update(updateHomework);
                var result = await repository.GetByIdAsync(1);
                await context.SaveChangesAsync();
                Assert.That(result.Task, Is.EqualTo("Update"));
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
                var repository = new HomeworkRepository(context);
                var newHomework = new Homework { Mark = 0, Task ="Test33", LectureId = 10, StudentId =50 };
                var contains = await repository.Contains(newHomework);
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
                var repository = new HomeworkRepository(context);
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
                var repository = new HomeworkRepository(context);
                var single = await repository.SingleOrDefaultAsync(c => c.Id == 1);
                Assert.That(single.Id, Is.EqualTo(1));
            }
        }
    }
}
