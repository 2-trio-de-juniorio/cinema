using Microsoft.AspNetCore.Diagnostics;

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

            httpContext.Response.StatusCode = statusCode;
            var response = new {Message = ex.Message};
            await httpContext.Response.WriteAsJsonAsync(response, token);

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
    }
}