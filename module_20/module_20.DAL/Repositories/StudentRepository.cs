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
    /// Class that executes the contract of a student repository
    /// </summary>
    public class StudentRepository : IRepository<Student>, IStudentRepository
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Constuctor with specified context
        /// </summary>
        /// <param name="context">Application context</param>
        public StudentRepository(ApplicationContext context)
        {
            _db = context ?? throw new ArgumentException(nameof(context));
        }

        /// <summary>
        /// Add a new element to repository
        /// </summary>
        /// <param name="entity">The new element that need to add</param>
        /// <returns>Task with new student object as a result</returns>
        public async Task<Student> AddAsync(Student entity)
        {
            var result = await _db.Students.AddAsync(entity);
            return result.Entity;
        }

        /// <summary>
        /// Add collection of elements to repository
        /// </summary>
        /// <param name="entities">Collection of elements that need to add</param>
        /// <returns>Task</returns>
        public async Task AddRangeAsync(IEnumerable<Student> entities)
        {
            await _db.Students.AddRangeAsync(entities);
        }

        /// <summary>
        /// Determines whether a sequence contains a specified element.
        /// </summary>
        /// <param name="entity">The value to locate in the sequence.</param>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        public async Task<bool> Contains(Student entity)
        {
            var result = await _db.Students.ContainsAsync(entity);
            return result;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An IEnumerable<T> that contains elements from the input sequence that satisfy the condition.</returns>
        public IEnumerable<Student> Where(Expression<Func<Student, bool>> predicate)
        {
            var students = _db.Students.Where(predicate);
            return students;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns>IQueryable collection of students</returns>
        public IQueryable<Student> GetAll()
        {
            return _db.Students;
        }

        /// <summary>
        /// Get the students with courses
        /// </summary>
        /// <returns>IQueryable collection of students with courses collection</returns>
        public IQueryable<Student> GetAllWithCourses()
        {
            return _db.Students
                .Include(sc => sc.StudentCourses)
                .ThenInclude(c => c.Course)
                .ThenInclude(l => l.Lecturer);
        }

        /// <summary>
        /// Get the students with courses and lectures
        /// </summary>
        /// <returns>IQueryable collection of students with lectures and courses collection</returns>
        public IQueryable<Student> GetAllWithLecturesAndCourses()
        {
            return _db.Students
               .Include(sc => sc.StudentCourses)
               .ThenInclude(c => c.Course)
               .ThenInclude(l => l.Lecturer)
               .Include(sl => sl.StudentLectures);
        }

        /// <summary>
        /// Get the students with lectures
        /// </summary>
        /// <returns>IQueryable collection of students with lectures collection</returns>
        public IQueryable<Student> GetAllWithLectures()
        {
            return _db.Students
               .Include(sl => sl.StudentLectures)
               .ThenInclude(l => l.Lecture);
        }

        /// <summary>
        /// Get the student with specified id
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        public async ValueTask<Student> GetByIdAsync(int id)
        {
            var student = await _db.Students.FindAsync(id);
            return student;
        }

        /// <summary>
        /// Get the student with specified id with courses
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        public async Task<Student> GetWithCoursesByIdAsync(int id)
        {
            var student = await _db.Students
                .Include(sc => sc.StudentCourses)
                .ThenInclude(c => c.Course)
                .ThenInclude(l => l.Lecturer)
                .FirstOrDefaultAsync(s => s.Id == id);
            return student;
        }

        /// <summary>
        /// Get the student with specified id with courses and lectures
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        public async Task<Student> GetWithLecturesAndCoursesByIdAsync(int id)
        {
            var student = await _db.Students
               .Include(sc => sc.StudentCourses)
               .ThenInclude(c => c.Course)
               .ThenInclude(l => l.Lecturer)
               .Include(sl => sl.StudentLectures)
               .FirstOrDefaultAsync(s => s.Id == id);
            return student;
        }

        /// <summary>
        /// Get the student with specified id with lectures
        /// </summary>
        /// <param name="id">Id of student</param>
        /// <returns>Task with the student object as result</returns>
        public async Task<Student> GetWithLecturesByIdAsync(int id)
        {
            var student = await _db.Students
              .Include(sl => sl.StudentLectures)
              .ThenInclude(l => l.Lecture)
              .FirstOrDefaultAsync(s => s.Id == id);
            return student;
        }

        /// <summary>
        /// Remove element from repository
        /// </summary>
        /// <param name="entity">The element that need to remove</param>
        public void Remove(Student entity)
        {
            _db.Students.Attach(entity);
            _db.Students.Remove(entity);
        }

        /// <summary>
        /// Remove collection of elements from repository
        /// </summary>
        /// <param name="entities">The collection of elements that need to remove</param>
        public void RemoveRange(IEnumerable<Student> entities)
        {
            _db.Students.RemoveRange(entities);
        }

        /// <summary>
        /// Returns a single, specific element of a sequence, or a default value if that element is not found.
        /// </summary>
        /// <param name="predicate">>A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        public async Task<Student> SingleOrDefaultAsync(Expression<Func<Student, bool>> predicate)
        {
            var student = await _db.Students.SingleOrDefaultAsync(predicate);
            return student;
        }

        /// <summary>
        /// Update a existing element in repository
        /// </summary>
        /// <param name="entity">The updated element</param>
        public async void Update(Student entity)
        {
            var oldEntity = await _db.Students.FindAsync(entity.Id);
            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Entry(entity).State = EntityState.Modified;
        }

    }
}
