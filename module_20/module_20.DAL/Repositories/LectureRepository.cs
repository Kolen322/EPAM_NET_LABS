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
    /// Class that executes the contract of a lecture repository
    /// </summary>
    public class LectureRepository : IRepository<Lecture>, ILectureRepository
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Constuctor with specified context
        /// </summary>
        /// <param name="context">Application context</param>
        public LectureRepository(ApplicationContext context)
        {
            _db = context ?? throw new ArgumentException(nameof(context));
        }

        /// <summary>
        /// Add a new element to repository
        /// </summary>
        /// <param name="entity">The new element that need to add</param>
        /// <returns>Task with new lecture object as a result</returns>
        public async Task<Lecture> AddAsync(Lecture entity)
        {
            var result = await _db.Lectures.AddAsync(entity);
            return result.Entity;
        }

        /// <summary>
        /// Add collection of elements to repository
        /// </summary>
        /// <param name="entities">Collection of elements that need to add</param>
        /// <returns>Task</returns>
        public async Task AddRangeAsync(IEnumerable<Lecture> entities)
        {
            await _db.Lectures.AddRangeAsync(entities);
        }

        /// <summary>
        /// Determines whether a sequence contains a specified element.
        /// </summary>
        /// <param name="entity">The value to locate in the sequence.</param>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        public async Task<bool> Contains(Lecture entity)
        {
            var result = await _db.Lectures.ContainsAsync(entity);
            return result;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An IEnumerable<T> that contains elements from the input sequence that satisfy the condition.</returns>
        public IEnumerable<Lecture> Where(Expression<Func<Lecture, bool>> predicate)
        {
            var lectures = _db.Lectures.Where(predicate);
            return lectures;
        }

        /// <summary>
        /// Get all lectures
        /// </summary>
        /// <returns>IQueryable collection of lectures</returns>
        public IQueryable<Lecture> GetAll()
        {
            return _db.Lectures;
        }

        /// <summary>
        /// Get the lectures with course
        /// </summary>
        /// <returns>IQueryable collection of lectures with course object</returns>
        public IQueryable<Lecture> GetAllWithCourse()
        {
            return _db.Lectures.Include(c => c.Course);
        }

        /// <summary>
        /// Get the lectures with homeworks
        /// </summary>
        /// <returns>IQueryable collection of lectures with homeworks collection</returns>
        public IQueryable<Lecture> GetAllWithHomeworks()
        {
            return _db.Lectures.Include(h => h.Homeworks);
        }

        /// <summary>
        /// Get the lectures with students
        /// </summary>
        /// <returns>IQueryable collection of lectures with students collection</returns>
        public IQueryable<Lecture> GetAllWithStudents()
        {
            return _db.Lectures
                .Include(sl => sl.StudentLectures)
                .ThenInclude(s => s.Student);
        }

        /// <summary>
        /// Get the lecture with specified id
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        public async ValueTask<Lecture> GetByIdAsync(int id)
        {
            var lecture = await _db.Lectures.FindAsync(id);
            return lecture;
        }

        /// <summary>
        /// Get the lecture with specified id with course
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        public async Task<Lecture> GetLectureWithCourseById(int id)
        {
            var lecture = await _db.Lectures
                .Where(l=>l.Id == id)
                .Include(c => c.Course)
                .FirstOrDefaultAsync();
            return lecture;
        }

        /// <summary>
        /// Get the lecture with specified id with homeworks
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        public async Task<Lecture> GetLectureWithHomeworksById(int id)
        {
            var lecture = await _db.Lectures
                .Include(h => h.Homeworks)
                .FirstOrDefaultAsync(l => l.Id == id);
            return lecture;
        }

        /// <summary>
        /// Get the lecture with specified id with lecture and students
        /// </summary>
        /// <param name="id">Id of lecture</param>
        /// <returns>Task with the lecture object as result</returns>
        public async Task<Lecture> GetLectureWithStudentsById(int id)
        {
            var lecture = await _db.Lectures
                .Include(sl => sl.StudentLectures)
                .ThenInclude(s => s.Student)
                .FirstOrDefaultAsync(l => l.Id == id);
            return lecture;
        }

        /// <summary>
        /// Remove element from repository
        /// </summary>
        /// <param name="entity">The element that need to remove</param>
        public void Remove(Lecture entity)
        {
            _db.Lectures.Remove(entity);
        }

        /// <summary>
        /// Remove collection of elements from repository
        /// </summary>
        /// <param name="entities">The collection of elements that need to remove</param>
        public void RemoveRange(IEnumerable<Lecture> entities)
        {
            _db.Lectures.RemoveRange(entities);
        }

        /// <summary>
        /// Returns a single, specific element of a sequence, or a default value if that element is not found.
        /// </summary>
        /// <param name="predicate">>A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        public async Task<Lecture> SingleOrDefaultAsync(Expression<Func<Lecture, bool>> predicate)
        {
            var lecture = await _db.Lectures.SingleOrDefaultAsync(predicate);
            return lecture;
        }

        /// <summary>
        /// Update a existing element in repository
        /// </summary>
        /// <param name="entity">The updated element</param>
        public async void Update(Lecture entity)
        {
            var oldEntity = await _db.Lectures.FindAsync(entity.Id);
            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
