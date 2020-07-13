using System;
using System.Net;

namespace module_20.BLL.Infrastructure.Exceptions
{
    /// <summary>
    /// Entity null exception
    /// </summary>
    public class EntityNullException : BaseExceptionModel
    {
        private static readonly string _defaultMessage = "The passed entity is null";

        /// <summary>
        /// Base constuctor with default message
        /// </summary>
        public EntityNullException() : base(_defaultMessage)
        {
            Initialize();
        }

        /// <summary>
        /// Constructor with specified message
        /// </summary>
        /// <param name="message">Message of exception</param>
        public EntityNullException(string message) : base(message)
        {
            Initialize();
        }

        /// <summary>
        /// Constructor with specified message and innerException
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception</param>
        public EntityNullException(string message, Exception innerException)
            : base(message, innerException)
        {
            Initialize();
        }

        private void Initialize()
        {
            base.StatusCode = HttpStatusCode.BadRequest;
        }

    }
}
