using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M08.Library.Collections
{
    /// <summary>
    /// Represents a set of values.
    /// </summary>
    /// <typeparam name="T">The type of elements in the set</typeparam>
    public class CustomSet<T> : IEnumerable<T> where T : class
    {
        private T[] _container;

        /// <summary>
        /// Get the number of elements contained in the set
        /// </summary>
        public int Count => _container.Length;

        /// <summary>
        /// Initializes a new instance of the CustomSet<T> class that is empty
        /// </summary>
        public CustomSet()
        {
            _container = new T[0];
        }

        /// <summary>
        /// Indicates whether the set is null
        /// </summary>
        /// <returns>Boolean value</returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

        /// <summary>
        /// Adds the specified element to a set
        /// </summary>
        /// <param name="item">The element to add to the set. The value can't be null</param>
        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!_container.Contains(item))
            {
                T[] newSet = new T[Count + 1];
                Array.Copy(_container, 0, newSet, 0, Count);
                newSet[Count] = item;
                _container = newSet;
            }
        }

        /// <summary>
        /// Removes the specified element from the set	
        /// </summary>
        /// <param name="item">The element to remove from the set. The value can't be null</param>
        public void Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!_container.Contains(item))
            {
                throw new KeyNotFoundException($"Element {item} don't exist in set");
            }
            T[] newSet = new T[Count - 1];
            Array.Copy(_container.Where((element) => element != item).ToArray(), 0, newSet, 0, Count - 1);
            _container = newSet;
        }

        /// <summary>
        /// Modifies the current Set to contain all elements that are present in itself, the specified collection, or both
        /// </summary>
        /// <param name="other">The collection to compare to the current Set</param>
        public void UnionWith(CustomSet<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            var resultSet = _container;

            for (int i = 0; i < other.Count; i++)
            {
                if (!resultSet.Contains(other._container[i]))
                {
                    resultSet = new T[Count + 1];
                    Array.Copy(_container, 0, resultSet, 0, Count);
                    resultSet[Count] = other._container[i];
                    _container = resultSet;
                }
            }

        }

        /// <summary>
        /// Modifies the current Set object to contain only elements that are present in that object and in the specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current Set</param>
        public void IntersectionWith(CustomSet<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            int countOfResultSet = 0;
            var resultSet = new T[countOfResultSet];

            for (int i = 0; i < Count; i++)
            {
                if (other._container.Contains(_container[i]))
                {
                    var newResultSet = new T[countOfResultSet + 1];
                    Array.Copy(resultSet, 0, newResultSet, 0, countOfResultSet);
                    newResultSet[countOfResultSet] = _container[i];
                    resultSet = newResultSet;
                    countOfResultSet++;
                }
            }
            _container = resultSet;

        }

        /// <summary>
        /// Removes all elements in the specified collection from the current Set object
        /// </summary>
        /// <param name="other">The collection of items to remove from the Set object</param>
        public void DifferenceWith(CustomSet<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            int countOfResultSet = 0;
            var resultSet = new T[countOfResultSet];

            for (int i = 0; i < Count; i++)
            {
                if (!other._container.Contains(_container[i]))
                {
                    var newResultSet = new T[countOfResultSet + 1];
                    Array.Copy(resultSet, 0, newResultSet, 0, countOfResultSet);
                    newResultSet[countOfResultSet] = _container[i];
                    resultSet = newResultSet;
                    countOfResultSet++;

                }
            }
            _container = resultSet;
        }

        /// <summary>
        /// Determines whether a Set object is a subset of the specified collection
        /// </summary>
        /// <param name="other">The collection to compare to the current Set object</param>
        /// <returns>True if the Set object is a subset of other or false, if isn't.</returns>
        public bool SubsetWith(CustomSet<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            for (int i = 0; i < Count; i++)
            {
                if (!other._container.Contains(_container[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection
        /// </summary>
        /// <returns>An IEnumerator<T> object</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _container[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
