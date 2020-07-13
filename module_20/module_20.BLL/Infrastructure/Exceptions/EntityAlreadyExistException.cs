using System;
using System.Net;

namespace module_20.BLL.Infrastructure.Exceptions
{
    /// <summary>
    /// Entity already exist in database exception
    /// </summary>
    public class EntityAlreadyExistException : BaseExceptionModel
    {
        private static readonly string _defaultMessage = "Entity already exist in the database";

        /// <summary>
        /// Base constuctor with default message
        /// </summary>
        public EntityAlreadyExistException() : base(_defaultMessage)
        {
            Initialize();
        }

        /// <summary>
        /// Constructor with specified message
        /// </summary>
        /// <param name="message">Message of exception</param>
        public EntityAlreadyExistException(string message) : base(message)
        {
            Initialize();
        }

        /// <summary>
        /// Constructor with specified message and innerException
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception</param>
        public EntityAlreadyExistException(string message, Exception innerException)
            : base(message, innerException)
        {
            Initialize();
        }

        private void Initialize()
        {
            base.StatusCode = HttpStatusCode.Conflict;
        }
    }
}
