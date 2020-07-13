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
    class CourseRepositoryTests
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
                var repository = new CourseRepository(context);
                var courses = repository.GetAll();
                Assert.That(courses.Count(), Is.EqualTo(context.Courses.Count()));
            }
        }

        [Test]
        public void GetCoursesWithLecturer_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new CourseRepository(context);
                var courses = repository.GetCoursesWithLecturer();
                Assert.That(courses.Count(), Is.EqualTo(context.Courses.Count()));
                Assert.That(courses.FirstOrDefault().Lecturer != null);
            }
        }

        [Test]
        public void GetCoursesWithLectures_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new CourseRepository(context);
                var courses = repository.GetCoursesWithLectures();
                Assert.That(courses.Count(), Is.EqualTo(context.Courses.Count()));
                Assert.That(courses.FirstOrDefault().Lectures != null);
            }
        }

        [Test]
        public void GetCoursesWithStudents_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new CourseRepository(context);
                var courses = repository.GetCoursesWithStudents();
                Assert.That(courses.Count(), Is.EqualTo(context.Courses.Count()));
                Assert.That(courses.FirstOrDefault().StudentCourses != null);
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
                var repository = new CourseRepository(context);
                var course = await repository.GetByIdAsync(id);
                Assert.That(course.Id, Is.EqualTo(id));
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseWithLecturerById_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new CourseRepository(context);
                var course = await repository.GetCourseWithLecturerById(id);
                Assert.That(course.Id, Is.EqualTo(id));
                Assert.That(course.Lecturer != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseWithLecturesById_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new CourseRepository(context);
                var course = await repository.GetCourseWithLecturesById(id);
                Assert.That(course.Id, Is.EqualTo(id));
                Assert.That(course.Lectures != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetCourseWithStudentsById_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new CourseRepository(context);
                var course = await repository.GetCourseWithStudentsById(id);
                Assert.That(course.Id, Is.EqualTo(id));
                Assert.That(course.StudentCourses != null);
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
                var repository = new CourseRepository(context);
                var newCourse = new Course { Name = "New", LecturerId = 1 };
                var prevCount = repository.GetAll().Count();
                await repository.AddAsync(newCourse);
                await context.SaveChangesAsync();
                var courses = repository.GetAll();
                Assert.That(courses.Count(), Is.EqualTo(prevCount+1));
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
                var repository = new CourseRepository(context);
                var newCourses = new List<Course>
                {
                    new Course
                    {
                        Name = "New",
                        LecturerId = 1
                    },
                    new Course
                    {
                        Name = "New",
                        LecturerId = 2
                    }
                };
                var prevCount = repository.GetAll().Count();
                await repository.AddRangeAsync(newCourses);
                await context.SaveChangesAsync();
                var courses = repository.GetAll();
                Assert.That(courses.Count(), Is.EqualTo(prevCount+2));
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
                var repository = new CourseRepository(context);
                var removeCourse = await repository.GetByIdAsync(1);
                var prevCount = repository.GetAll().Count();

                repository.Remove(removeCourse);

                await context.SaveChangesAsync();
                var courses = repository.GetAll();
                Assert.That(courses.Count(), Is.EqualTo(prevCount - 1));
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
                var repository = new CourseRepository(context);
                var removeCourses = new List<Course>();
                removeCourses.AddRange(repository.GetAll().ToList());

                repository.RemoveRange(removeCourses);
                await context.SaveChangesAsync();
                var courses = repository.GetAll();
                Assert.That(courses.Count(), Is.EqualTo(0));
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
                var repository = new CourseRepository(context);
                var updateCourse = await repository.GetByIdAsync(1);
                updateCourse.Name = "Update";

                repository.Update(updateCourse);
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
                var repository = new CourseRepository(context);
                var newCourse = new Course { Name = "Test", LecturerId = 1 };
                var contains = await repository.Contains(newCourse);
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
                var repository = new CourseRepository(context);
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
                var repository = new CourseRepository(context);
                var single = await repository.SingleOrDefaultAsync(c=>c.Id == 1);
                Assert.That(single.Id, Is.EqualTo(1));
            }
        }
    }
}
