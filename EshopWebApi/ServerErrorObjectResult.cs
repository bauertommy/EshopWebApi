using Microsoft.AspNetCore.Mvc;

namespace EshopWebApi
{
    /// <summary>
    /// Object result that encapsulates exceptions
    /// </summary>
    public class ServerErrorObjectResult : ObjectResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerErrorObjectResult"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="statusCode">HTTP status code</param>
        public ServerErrorObjectResult(object value, int statusCode) : base(value)
        {
            StatusCode = statusCode;
        }
    }
}
