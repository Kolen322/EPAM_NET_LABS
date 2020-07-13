using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M08.Library.Collections
{
    /// <summary>
    /// Represents a first-in, first-out collection of objects
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the queue</typeparam>
    public class CustomQueue<T> : ICustomEnumerator<T>
    {
        private T[] _container;
        private int _capacity;

        /// <summary>
        /// Get the number of elements contained in the queue
        /// </summary>
        public int Count => _container.Length;

        /// <summary>
        /// Initializes a new instance of the CustomQueue<T> class that is empty
        /// </summary>
        public CustomQueue()
        {
            _container = new T[0];
            _capacity = 0;
        }
        /// <summary>
        /// Initializes a new instance of the Queue<T> class that containt elements copied from the specified collection	
        /// </summary>
        /// <param name="container">Specified collection</param>
        public CustomQueue(T[] container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _capacity = container.Length;
        }

        /// <summary>
        /// Indicates whether the queue is null
        /// </summary>
        /// <returns>Boolean value</returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

        /// <summary>
        /// Adds an object to the end of the queue	
        /// </summary>
        /// <param name="item">The object to add to the queue. The value can't be null for reference type</param>
        public void Enqueue(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            T[] newQueue = new T[_capacity + 1];
            Array.Copy(_container, 0, newQueue, 0, _container.Length);
            _container = newQueue;
            _capacity++;
            _container[_capacity - 1] = item;
        }

        /// <summary>
        /// Removes and returns the object at the beginning of the queue
        /// </summary>
        /// <returns>The object that is removed from the beginning of the queue</returns>
        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            var returnItem = _container[0];
            _capacity--;
            T[] newQueue = new T[_capacity];
            Array.Copy(_container, 1, newQueue, 0, _container.Length - 1);
            _container = newQueue;
            return returnItem;
        }

        /// <summary>
        /// Returns the object at the beginning of the queue without removing it
        /// </summary>
        /// <returns>The object at the beginning of the queue</returns>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }
            return _container[0];
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection
        /// </summary>
        /// <returns>An ICustomeIterator<T> object</returns>
        public ICustomIterator<T> GetEnumerator()
        {
            return new ClassicIterator<T>(_container);
        }

    }
}
