using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using module_20.BLL.Infrastructure.Exceptions;
using System;
using System.Net;

namespace module_20.Web.Filters
{
    /// <summary>
    /// Class that realize exception filter
    /// </summary>
    public class ErrorResponseFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor with specefied logger
        /// </summary>
        /// <param name="logger">Logger</param>
        public ErrorResponseFilter(ILogger<ErrorResponseFilter> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Action when application throw exception
        /// </summary>
        /// <param name="context">Exception context</param>
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;

            response.ContentType = "application/json";

            if(context.Exception is BaseExceptionModel exceptionModel)
            {
                response.StatusCode = (int)exceptionModel.StatusCode;
                response.WriteAsync(exceptionModel.ToJson());
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.WriteAsync(context.Exception.Message);
            }
            _logger.LogError(DateTime.Now + $"\n{context.Exception}");
        }
    }
}
