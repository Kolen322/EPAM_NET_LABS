using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M08.Library.Collections
{
    /// <summary>
    /// Represents a variable size last-in-first-out (LIFO) collection of instances of the same specified type
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the stack</typeparam>
    public class CustomStack<T> : ICustomEnumerator<T>
    {
        private T[] _container;

        private int _capacity;

        /// <summary>
        /// Get the number of elements contained in the stack
        /// </summary>
        public int Count => _container.Length;

        /// <summary>
        /// Initializes a new instance of the Stack class that is empty	
        /// </summary>
        public CustomStack()
        {
            _container = new T[0];
            _capacity = 0;
        }

        /// <summary>
        /// Indicates whether the stack is null
        /// </summary>
        /// <returns>Boolean value</returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

        /// <summary>
        /// Inserts an object at the top of the Stack
        /// </summary>
        /// <param name="item">The object to push onto the Stack</param>
        public void Push(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            T[] newStack = new T[_capacity + 1];
            Array.Copy(_container, 0, newStack, 1, _container.Length);
            _container = newStack;
            _capacity++;
            _container[0] = item;
        }

        /// <summary>
        /// Removes and returns the object at the top of the Stack
        /// </summary>
        /// <returns>The object removed from the top of the Stack</returns>
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            T returnItem = _container[0];
            T[] newStack = new T[_capacity - 1];
            Array.Copy(_container, 1, newStack, 0, _container.Length - 1);
            _container = newStack;
            _capacity--;
            return returnItem;
        }

        /// <summary>
        /// Returns the object at the top of the Stack without removing it
        /// </summary>
        /// <returns>The object at the top of the Stack</returns>
        public T Peek()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Stack is empty");
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
