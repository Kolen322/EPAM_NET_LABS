using Microsoft.EntityFrameworkCore;
using module_20.DAL.Entities;
using module_20.DAL.EntityFramework;
using module_20.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace module_20.DAL.Repositories
{
    /// <summary>
    /// Class that executes the contract of a course repository
    /// </summary>
    public class CourseRepository : IRepository<Course>, ICourseRepository
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Constuctor with specified context
        /// </summary>
        /// <param name="context">Application context</param>
        public CourseRepository(ApplicationContext context)
        {
            _db = context ?? throw new ArgumentException(nameof(context));
        }

        /// <summary>
        /// Add a new course to repository
        /// </summary>
        /// <param name="entity">The new course that need to add</param>
        /// <returns>Task with new course object as a result</returns>
        public async Task<Course> AddAsync(Course entity)
        {
            var result = await _db.Courses.AddAsync(entity);
            return result.Entity;
        }

        /// <summary>
        /// Add collection of courses to repository
        /// </summary>
        /// <param name="entities">Collection of courses that need to add</param>
        /// <returns>Task</returns>
        public async Task AddRangeAsync(IEnumerable<Course> entities)
        {
            await _db.Courses.AddRangeAsync(entities);
        }

        /// <summary>
        /// Determines whether a sequence contains a specified element.
        /// </summary>
        /// <param name="entity">The value to locate in the sequence.</param>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        public async Task<bool> Contains(Course entity)
        {
            var result = await _db.Courses.ContainsAsync(entity);
            return result;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An IEnumerable<T> that contains elements from the input sequence that satisfy the condition.</returns>
        public IEnumerable<Course> Where(Expression<Func<Course, bool>> predicate)
        {
            var course = _db.Courses.Where(predicate);
            return course;
        }

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns>IQueryable collection of courses</returns>
        public IQueryable<Course> GetAll()
        {
            return _db.Courses;
        }

        /// <summary>
        /// Get the course with specified id
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the course object as result</returns>
        public async ValueTask<Course> GetByIdAsync(int id)
        {
            var course = await _db.Courses.FindAsync(id);
            return course;
        }

        /// <summary>
        /// Get the courses with lecturer
        /// </summary>
        /// <returns>IQueryable collection of courses with lecturer object</returns>
        public IQueryable<Course> GetCoursesWithLecturer()
        {
            return _db.Courses.Include(l=>l.Lecturer);
        }
        /// <summary>
        /// Get the courses with lectures
        /// </summary>
        /// <returns>IQueryable collection of courses with lectures collection</returns>
        public IQueryable<Course> GetCoursesWithLectures()
        {
            return _db.Courses.Include(l => l.Lectures);
        }

        /// <summary>
        /// Get the courses with students
        /// </summary>
        /// <returns>IQueryable collection of courses with students collection</returns>
        public IQueryable<Course> GetCoursesWithStudents()
        {
            return _db.Courses
                .Include(sc => sc.StudentCourses)
                .ThenInclude(s => s.Student);
        }

        /// <summary>
        /// Get the course with specified id with lecturer
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the course object as result</returns>
        public async Task<Course> GetCourseWithLecturerById(int id)
        {
            var course = await _db.Courses.Include(l => l.Lecturer).FirstOrDefaultAsync(c => c.Id == id);
            return course;
        }

        /// <summary>
        /// Get the course with specified id with lectures
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the course object as result</returns>
        public async Task<Course> GetCourseWithLecturesById(int id)
        {
            var course = await _db.Courses.Include(l => l.Lectures).FirstOrDefaultAsync(c => c.Id == id);
            return course;
        }

        /// <summary>
        /// Get the course with specified id with students
        /// </summary>
        /// <param name="id">Id of course</param>
        /// <returns>Task with the course object as result</returns>
        public async Task<Course> GetCourseWithStudentsById(int id)
        {
            var course = await _db.Courses
                .Include(sc => sc.StudentCourses)
                .ThenInclude(s => s.Student)
                .FirstOrDefaultAsync(c => c.Id == id);
            return course;
        }

        /// <summary>
        /// Remove element from repository
        /// </summary>
        /// <param name="entity">The element that need to remove</param>
        public void Remove(Course entity)
        {
            _db.Courses.Remove(entity);
        }

        /// <summary>
        /// Remove collection of elements from repository
        /// </summary>
        /// <param name="entities">The collection of elements that need to remove</param>
        public void RemoveRange(IEnumerable<Course> entities)
        {
            _db.Courses.RemoveRange(entities);
        }

        /// <summary>
        /// Returns a single, specific element of a sequence, or a default value if that element is not found.
        /// </summary>
        /// <param name="predicate">>A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        public async Task<Course> SingleOrDefaultAsync(Expression<Func<Course, bool>> predicate)
        {
            var course = await _db.Courses.SingleOrDefaultAsync(predicate);
            return course;
        }

        /// <summary>
        /// Update a existing element in repository
        /// </summary>
        /// <param name="entity">The updated element</param>
        public async void Update(Course entity)
        {
            var oldEntity = await _db.Courses.FindAsync(entity.Id);
            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
