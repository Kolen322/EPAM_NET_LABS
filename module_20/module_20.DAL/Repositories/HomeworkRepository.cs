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
    /// Class that executes the contract of a homework repository
    /// </summary>
    public class HomeworkRepository : IRepository<Homework>, IHomeworkRepository
    {
        private readonly ApplicationContext _db;

        /// <summary>
        /// Constuctor with specified context
        /// </summary>
        /// <param name="context">Application context</param>
        public HomeworkRepository(ApplicationContext context)
        {
            _db = context ?? throw new ArgumentException(nameof(context));
        }

        /// <summary>
        /// Add a new element to repository
        /// </summary>
        /// <param name="entity">The new element that need to add</param>
        /// <returns>Task with new homework object as a result</returns>
        public async Task<Homework> AddAsync(Homework entity)
        {
            var result = await _db.Homeworks.AddAsync(entity);
            return result.Entity;
        }

        /// <summary>
        /// Add collection of elements to repository
        /// </summary>
        /// <param name="entities">Collection of elements that need to add</param>
        /// <returns>Task</returns>
        public async Task AddRangeAsync(IEnumerable<Homework> entities)
        {
            await _db.Homeworks.AddRangeAsync(entities);
        }

        /// <summary>
        /// Determines whether a sequence contains a specified element.
        /// </summary>
        /// <param name="entity">The value to locate in the sequence.</param>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        public async Task<bool> Contains(Homework entity)
        {
            var result = await _db.Homeworks.ContainsAsync(entity);
            return result;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An IEnumerable<T> that contains elements from the input sequence that satisfy the condition.</returns>
        public IEnumerable<Homework> Where(Expression<Func<Homework, bool>> predicate)
        {
            var homework = _db.Homeworks.Where(predicate);
            return homework;
        }

        /// <summary>
        /// Get all homeworks
        /// </summary>
        /// <returns>IQueryable collection of homeworks</returns>
        public IQueryable<Homework> GetAll()
        {
            return _db.Homeworks;
        }

        /// <summary>
        /// Get the homeworks with lecture
        /// </summary>
        /// <returns>IQueryable collection of homeworks with lecture object</returns>
        public IQueryable<Homework> GetAllWithLecture()
        {
            return _db.Homeworks.Include(l => l.Lecture);
        }

        /// <summary>
        /// Get the homeworks with lecture
        /// </summary>
        /// <returns>IQueryable collection of homeworks with student and lecture object</returns>
        public IQueryable<Homework> GetAllWithLectureAndStudent()
        {
            return _db.Homeworks
                .Include(s => s.Student)
                .Include(l => l.Lecture);
        }

        /// <summary>
        /// Get the homeworks with student
        /// </summary>
        /// <returns>IQueryable collection of homeworks with student object</returns>
        public IQueryable<Homework> GetAllWithStudent()
        {
            return _db.Homeworks.Include(s => s.Student);
        }

        /// <summary>
        /// Get the homework with specified id
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        public ValueTask<Homework> GetByIdAsync(int id)
        {
            var homework = _db.Homeworks.FindAsync(id);
            return homework;
        }

        /// <summary>
        /// Get the homework with specified id with lecture and student
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        public async Task<Homework> GetHomeworkWithLectureAndStudentById(int id)
        {
            var homework = await _db.Homeworks
                .Include(l => l.Lecture)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(h => h.Id == id);
            return homework;
        }

        /// <summary>
        /// Get the homework with specified id with lecture
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        public async Task<Homework> GetHomeworkWithLectureById(int id)
        {
            var homework = await _db.Homeworks
                .Include(l => l.Lecture)
                .FirstOrDefaultAsync(h => h.Id == id);
            return homework;
        }

        /// <summary>
        /// Get the homework with specified id with student
        /// </summary>
        /// <param name="id">Id of homework</param>
        /// <returns>Task with the homework object as result</returns>
        public async Task<Homework> GetHomeworkWithStudentById(int id)
        {
            var homework = await _db.Homeworks
                .Include(s => s.Student)
                .FirstOrDefaultAsync(h => h.Id == id);
            return homework;
        }

        /// <summary>
        /// Remove element from repository
        /// </summary>
        /// <param name="entity">The element that need to remove</param>
        public void Remove(Homework entity)
        {
            _db.Homeworks.Remove(entity);
        }

        /// <summary>
        /// Remove collection of elements from repository
        /// </summary>
        /// <param name="entities">The collection of elements that need to remove</param>
        public void RemoveRange(IEnumerable<Homework> entities)
        {
            _db.Homeworks.RemoveRange(entities);
        }

        /// <summary>
        /// Returns a single, specific element of a sequence, or a default value if that element is not found.
        /// </summary>
        /// <param name="predicate">>A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        public async Task<Homework> SingleOrDefaultAsync(Expression<Func<Homework, bool>> predicate)
        {
            var homework = await _db.Homeworks.SingleOrDefaultAsync(predicate);
            return homework;
        }

        /// <summary>
        /// Update a existing element in repository
        /// </summary>
        /// <param name="entity">The updated element</param>
        public async void Update(Homework entity)
        {
            var oldEntity = await _db.Homeworks.FindAsync(entity.Id);
            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Entry(entity).State = EntityState.Modified;
        }
    }
}
