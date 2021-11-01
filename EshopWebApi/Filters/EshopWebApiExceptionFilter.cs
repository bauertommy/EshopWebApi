using EshopWebApi.BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace EshopWebApi.Filters
{
    /// <summary>
    /// Exception filter that runs after an action has thrown an System.Exception.
    /// </summary>
    public class EshopWebApiExceptionFilter : ExceptionFilterAttribute
    {
        ///<inheritdoc/>
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Result = new ServerErrorObjectResult(context.Exception.Message, StatusCodes.Status404NotFound);
                return;
            }

            if (context.Exception is ArgumentNullException)
            {
                context.Result = new ServerErrorObjectResult(context.Exception.Message, StatusCodes.Status400BadRequest);
                return;
            }

            context.Result = new ServerErrorObjectResult(context.Exception.Message, StatusCodes.Status500InternalServerError);
        }
    }
}
