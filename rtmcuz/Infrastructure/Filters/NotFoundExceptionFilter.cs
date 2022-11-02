using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using rtmcuz.Infrastructure.Exceptions;
using System.Net;
using System.Threading;

namespace rtmcuz.Infrastructure.Filters
{
    public class NotFoundExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is NotFoundException e)
            {
                context.Result = new JsonResult(e.Message)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is NotFoundException)
            {
                context.Result = new NotFoundResult();
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
