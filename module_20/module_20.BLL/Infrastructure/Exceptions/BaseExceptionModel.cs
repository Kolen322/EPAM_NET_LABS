using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace module_20.BLL.Infrastructure.Exceptions
{
    /// <summary>
    /// Model that represent api exception
    /// </summary>
    public abstract class BaseExceptionModel : Exception
    {
        /// <summary>
        /// Http status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Base constructor
        /// </summary>
        public BaseExceptionModel()
        {

        }

        /// <summary>
        /// Constructor with specified message
        /// </summary>
        /// <param name="message">Message of exception</param>
        public BaseExceptionModel(string message) : base(message)
        {

        }

        /// <summary>
        /// Constructor with specified message and innerException
        /// </summary>
        /// <param name="message">Message of exception</param>
        /// <param name="innerException">Inner exception</param>
        public BaseExceptionModel(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }

        /// <summary>
        /// Convert exception to json
        /// </summary>
        /// <returns>String with json</returns>
        public string ToJson()
        {
            return (new JObject
            {
                new JProperty(nameof(Message), Message),
                new JProperty(nameof(Type), GetType().ToString()),
                new JProperty(nameof(HttpStatusCode), StatusCode)
            }).ToString();
        }
    }
}
