using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CinemaWebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="ex"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken token)
        {
            int statusCode = getStatusCode(ex);

            ProblemDetails problemDetails = createProblemDetails(ex, statusCode);

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, token);

            return true;
        }
        private int getStatusCode(Exception ex) 
        {
            return ex switch 
            {
                ArgumentException or BadHttpRequestException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

                _ => StatusCodes.Status500InternalServerError
            };
        }
        private ProblemDetails createProblemDetails(Exception ex, int statusCode)
        {
            string reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);

            if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest);
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = reasonPhrase,
                Detail = ex.Message
            };

            return problemDetails;
        }
    }
}