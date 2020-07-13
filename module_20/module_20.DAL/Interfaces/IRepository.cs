using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace module_20.DAL.Interfaces
{
    /// <summary>
    /// Specifies the contract for a repository pattern
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get element by id
        /// </summary>
        /// <param name="id">Id of element</param>
        /// <returns>Object if element exist or null if doesn't</returns>
        ValueTask<TEntity> GetByIdAsync(int id);
        /// <summary>
        /// Get all elements from repository
        /// </summary>
        /// <returns>IQueryable collection with all elements from repository</returns>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An IEnumerable<T> that contains elements from the input sequence that satisfy the condition.</returns>
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Returns a single, specific element of a sequence, or a default value if that element is not found.
        /// </summary>
        /// <param name="predicate">>A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Determines whether a sequence contains a specified element.
        /// </summary>
        /// <param name="entity">The value to locate in the sequence.</param>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        Task<bool> Contains(TEntity entity);
        /// <summary>
        /// Add a new element to repository
        /// </summary>
        /// <param name="entity">The new element that need to add</param>
        /// <returns>Task witn new element as a result</returns>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// Add collection of elements to repository
        /// </summary>
        /// <param name="entities">Collection of elements that need to add</param>
        /// <returns>Task</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Remove element from repository
        /// </summary>
        /// <param name="entity">The element that need to remove</param>
        void Remove(TEntity entity);
        /// <summary>
        /// Remove collection of elements from repository
        /// </summary>
        /// <param name="entities">The collection of elements that need to remove</param>
        void RemoveRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// Update a existing element in repository
        /// </summary>
        /// <param name="entity">The updated element</param>
        void Update(TEntity entity);

    }
}
