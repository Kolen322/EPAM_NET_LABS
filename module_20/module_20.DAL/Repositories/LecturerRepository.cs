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
    /// Class that executes the contract of a lecturer repository
    /// </summary>
    public class LecturerRepository : IRepository<Lecturer>, ILecturerRepository
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Constuctor with specified context
        /// </summary>
        /// <param name="context">Application context</param>
        public LecturerRepository(ApplicationContext context)
        {
            _db = context ?? throw new ArgumentException(nameof(context));
        }

        /// <summary>
        /// Add a new element to repository
        /// </summary>
        /// <param name="entity">The new element that need to add</param>
        /// <returns>Task with new lecturer object as a result</returns>
        public async Task<Lecturer> AddAsync(Lecturer entity)
        {
            var result = await _db.Lecturers.AddAsync(entity);
            return result.Entity;
        }

        /// <summary>
        /// Add collection of elements to repository
        /// </summary>
        /// <param name="entities">Collection of elements that need to add</param>
        /// <returns>Task</returns>
        public async Task AddRangeAsync(IEnumerable<Lecturer> entities)
        {
            await _db.Lecturers.AddRangeAsync(entities);
        }

        /// <summary>
        /// Determines whether a sequence contains a specified element.
        /// </summary>
        /// <param name="entity">The value to locate in the sequence.</param>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        public async Task<bool> Contains(Lecturer entity)
        {
            var result = await _db.Lecturers.ContainsAsync(entity);
            return result;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An IEnumerable<T> that contains elements from the input sequence that satisfy the condition.</returns>
        public IEnumerable<Lecturer> Where(Expression<Func<Lecturer, bool>> predicate)
        {
            var lecturers = _db.Lecturers.Where(predicate);
            return _db.Lecturers.Where(predicate);
        }

        /// <summary>
        /// Get all lecturers
        /// </summary>
        /// <returns>IQueryable collection of lecturers</returns>
        public IQueryable<Lecturer> GetAll()
        {
            return _db.Lecturers;
        }

        /// <summary>
        /// Get the lecturer with specified id
        /// </summary>
        /// <param name="id">Id of lecturer</param>
        /// <returns>Task with the lecturer object as result</returns>
        public ValueTask<Lecturer> GetByIdAsync(int id)
        {
            return _db.Lecturers.FindAsync(id);
        }

        /// <summary>
        /// Get the lecturers with courses
        /// </summary>
        /// <returns>IQueryable collection of lectures with courses collection</returns>
        public IQueryable<Lecturer> GetLecturersWithCourses()
        {
            return _db.Lecturers.Include(c => c.Courses);
        }

        /// <summary>
        /// Get the lecturer with specified id with courses
        /// </summary>
        /// <param name="id">Id of lecturer</param>
        /// <returns>Task with the lecturer object as result</returns>
        public async Task<Lecturer> GetLecturerWithCoursesByIdAsync(int id)
        {
            var lecturer = await _db.Lecturers
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);
            return lecturer;
        }

        /// <summary>
        /// Remove element from repository
        /// </summary>
        /// <param name="entity">The element that need to remove</param>
        public void Remove(Lecturer entity)
        {
            _db.Lecturers.Remove(entity);
        }

        /// <summary>
        /// Remove collection of elements from repository
        /// </summary>
        /// <param name="entities">The collection of elements that need to remove</param>
        public void RemoveRange(IEnumerable<Lecturer> entities)
        {
            _db.Lecturers.RemoveRange(entities);
        }

        /// <summary>
        /// Returns a single, specific element of a sequence, or a default value if that element is not found.
        /// </summary>
        /// <param name="predicate">>A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        public async Task<Lecturer> SingleOrDefaultAsync(Expression<Func<Lecturer, bool>> predicate)
        {
            var lecturer = await _db.Lecturers.SingleOrDefaultAsync(predicate);
            return lecturer;
        }

        /// <summary>
        /// Update a existing element in repository
        /// </summary>
        /// <param name="entity">The updated element</param>
        public async void Update(Lecturer entity)
        {

            var oldEntity = await _db.Lecturers.FindAsync(entity.Id);
            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
