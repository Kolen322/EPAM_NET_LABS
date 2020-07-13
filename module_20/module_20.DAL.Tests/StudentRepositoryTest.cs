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
    class StudentRepositoryTest
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
                var repository = new StudentRepository(context);
                var students = repository.GetAll();
                Assert.That(students.Count(), Is.EqualTo(context.Students.Count()));
            }
        }

        [Test]
        public void GetAllWithCourses_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new StudentRepository(context);
                var students = repository.GetAllWithCourses();
                Assert.That(students.Count(), Is.EqualTo(context.Students.Count()));
                Assert.That(students.FirstOrDefault().StudentCourses != null);
            }
        }

        [Test]
        public void GetAllWithLectures_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new StudentRepository(context);
                var students = repository.GetAllWithLectures();
                Assert.That(students.Count(), Is.EqualTo(context.Students.Count()));
                Assert.That(students.FirstOrDefault().StudentLectures != null);
            }
        }

        [Test]
        public void GetAllWithLecturesAndCourses_Test()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "GetTestDatabase")
                .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new StudentRepository(context);
                var students = repository.GetAllWithLecturesAndCourses();
                Assert.That(students.Count(), Is.EqualTo(context.Students.Count()));
                Assert.That(students.FirstOrDefault().StudentLectures != null);
                Assert.That(students.FirstOrDefault().StudentCourses != null);
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
                var repository = new StudentRepository(context);
                var student = await repository.GetByIdAsync(id);
                Assert.That(student.Id, Is.EqualTo(id));
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetWithLecturesByIdAsync_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new StudentRepository(context);
                var student = await repository.GetWithLecturesByIdAsync(id);
                Assert.That(student.Id, Is.EqualTo(id));
                Assert.That(student.StudentLectures != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetWithCoursesByIdAsync_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new StudentRepository(context);
                var student = await repository.GetWithCoursesByIdAsync(id);
                Assert.That(student.Id, Is.EqualTo(id));
                Assert.That(student.StudentCourses != null);
            }
        }

        [Test]
        [TestCase(1)]
        public async Task GetWithLecturesAndCoursesByIdAsync_Test(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: "GetTestDatabase")
               .Options;

            using (var context = new ApplicationContext(options))
            {
                context.Database.EnsureCreated();
                var repository = new StudentRepository(context);
                var student = await repository.GetWithLecturesAndCoursesByIdAsync(id);
                Assert.That(student.Id, Is.EqualTo(id));
                Assert.That(student.StudentLectures != null);
                Assert.That(student.StudentCourses != null);
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
                var repository = new StudentRepository(context);
                var newStudent = new Student { Email = "Test", Mobile = "Test", Name = "Test" };
                var prevCount = repository.GetAll().Count();
                await repository.AddAsync(newStudent);
                await context.SaveChangesAsync();
                var students = repository.GetAll();
                Assert.That(students.Count(), Is.EqualTo(prevCount + 1));
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
                var repository = new StudentRepository(context);
                var newStudents = new List<Student>
                {
                    new Student
                    {
                        Name = "Test",
                        Mobile = "Q",
                        Email = "Test"
                    },
                    new Student
                    {
                        Name = "Test",
                        Mobile = "Q",
                        Email = "Test"
                    }
                };
                var prevCount = repository.GetAll().Count();
                await repository.AddRangeAsync(newStudents);
                await context.SaveChangesAsync();
                var students = repository.GetAll();
                Assert.That(students.Count(), Is.EqualTo(prevCount + 2));
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
                var repository = new StudentRepository(context);
                var removeStudent = await repository.GetByIdAsync(1);
                var prevCount = repository.GetAll().Count();

                repository.Remove(removeStudent);

                await context.SaveChangesAsync();
                var students = repository.GetAll();
                Assert.That(students.Count(), Is.EqualTo(prevCount - 1));
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
                var repository = new StudentRepository(context);
                var removeStudents = new List<Student>();
                removeStudents.AddRange(repository.GetAll().ToList());

                repository.RemoveRange(removeStudents);
                await context.SaveChangesAsync();
                var students = repository.GetAll();
                Assert.That(students.Count(), Is.EqualTo(0));
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
                var repository = new StudentRepository(context);
                var updateStudent = await repository.GetByIdAsync(1);
                updateStudent.Name = "Update";

                repository.Update(updateStudent);
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
                var repository = new StudentRepository(context);
                var newStudent = new Student { Name = "Q" };
                var contains = await repository.Contains(newStudent);
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
                var repository = new StudentRepository(context);
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
                var repository = new StudentRepository(context);
                var single = await repository.SingleOrDefaultAsync(c => c.Id == 1);
                Assert.That(single.Id, Is.EqualTo(1));
            }
        }
    }
}
