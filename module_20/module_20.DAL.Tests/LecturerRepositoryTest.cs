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
    class LecturerRepositoryTest
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
                var repository = new LecturerRepository(context);
                var lecturers = repository.GetAll();
                Assert.That(lecturers.Count(), Is.EqualTo(context.Lecturers.Count()));
            }
        }

        [Test]
        public void GetLecturersWithCourses_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LecturerRepository(context);
                var lecturers = repository.GetLecturersWithCourses();
                Assert.That(lecturers.Count(), Is.EqualTo(context.Lecturers.Count()));
                Assert.That(lecturers.FirstOrDefault().Courses != null);
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
                var repository = new LecturerRepository(context);
                var lecturer = await repository.GetByIdAsync(id);
                Assert.That(lecturer.Id, Is.EqualTo(id));
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetLecturerWithCoursesIdAsync_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new LecturerRepository(context);
                var lecturer = await repository.GetLecturerWithCoursesByIdAsync(id);
                Assert.That(lecturer.Id, Is.EqualTo(id));
                Assert.That(lecturer.Courses != null);
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
                var repository = new LecturerRepository(context);
                var newLecturer = new Lecturer { Name = "Test" };
                var prevCount = repository.GetAll().Count();
                await repository.AddAsync(newLecturer);
                await context.SaveChangesAsync();
                var lecturers = repository.GetAll();
                Assert.That(lecturers.Count(), Is.EqualTo(prevCount + 1));
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
                var repository = new LecturerRepository(context);
                var newLecturers = new List<Lecturer>
                {
                    new Lecturer
                    {
                        Name = "Test"
                    },
                    new Lecturer
                    {
                        Name = "Test"
                    }
                };
                var prevCount = repository.GetAll().Count();
                await repository.AddRangeAsync(newLecturers);
                await context.SaveChangesAsync();
                var lecturers = repository.GetAll();
                Assert.That(lecturers.Count(), Is.EqualTo(prevCount + 2));
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
                var repository = new LecturerRepository(context);
                var removeLecturer = await repository.GetByIdAsync(1);
                var prevCount = repository.GetAll().Count();

                repository.Remove(removeLecturer);

                await context.SaveChangesAsync();
                var lecturers = repository.GetAll();
                Assert.That(lecturers.Count(), Is.EqualTo(prevCount - 1));
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
                var repository = new LecturerRepository(context);
                var removeLecturers = new List<Lecturer>();
                removeLecturers.AddRange(repository.GetAll().ToList());

                repository.RemoveRange(removeLecturers);
                await context.SaveChangesAsync();
                var lecturers = repository.GetAll();
                Assert.That(lecturers.Count(), Is.EqualTo(0));
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
                var repository = new LecturerRepository(context);
                var updateLecturer = await repository.GetByIdAsync(1);
                updateLecturer.Name = "Update";

                repository.Update(updateLecturer);
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
                var repository = new LecturerRepository(context);
                var newLecturer = new Lecturer { Name = "TESTSTSTST" };
                var contains = await repository.Contains(newLecturer);
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
                var repository = new LecturerRepository(context);
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
                var repository = new LecturerRepository(context);
                var single = await repository.SingleOrDefaultAsync(c => c.Id == 1);
                Assert.That(single.Id, Is.EqualTo(1));
            }
        }
    }
}
