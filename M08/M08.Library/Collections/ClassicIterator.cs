using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M08.Library.Collections
{
    /// <summary>
    /// Supports a simple iteration over a generic collection
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate</typeparam>
    public class ClassicIterator<T> : ICustomIterator<T>
    {
        private T[] _container;
        private int _currentIndex;

        /// <summary>
        /// Initializes a new instance of the ClassicIterator class with empty container
        /// </summary>
        public ClassicIterator() { }

        /// <summary>
        /// Initializes a new instance of the ClassicIterator class with specified container
        /// </summary>
        /// <param name="list">Specified collection</param>
        public ClassicIterator(T[] list)
        {
            _currentIndex = -1;
            _container = list;
        }

        /// <summary>
        /// Gets the element in the collection at the current positon of the enumerator.	
        /// </summary>
        public T Current
        {
            get
            {
                if (_currentIndex < 0 || _currentIndex > _container.Length)
                    throw new InvalidOperationException();
                return _container[_currentIndex];
            }
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection	
        /// </summary>
        public void Reset()
        {
            _currentIndex = -1;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.	
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            if (_currentIndex < _container.Length)
            {
                _currentIndex++;
                return (_currentIndex < _container.Length);
            }
            return false;
        }



    }
}
