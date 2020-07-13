using System;
using System.Net;

namespace module_20.BLL.Infrastructure.Exceptions
{
    /// <summary>
    /// Entity not found in database exception
    /// </summary>
    public class EntityNotFoundException : BaseExceptionModel
    {
        private static readonly string _defaultMessage = "Entity doesn't exist in database";

        /// <summary>
        /// Base constuctor with default message
        /// </summary>
        public EntityNotFoundException() : base(_defaultMessage)
        {
            Initialize();
        }

        /// <summary>
        /// Constructor with specified message
        /// </summary>
        /// <param name="message">Message of exception</param>
        public EntityNotFoundException(string message) : base(message)
        {
            Initialize();
        }

        /// <summary>
        /// Constructor with specified message and innerException
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception</param>
        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
            Initialize();
        }

        private void Initialize()
        {
            base.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
