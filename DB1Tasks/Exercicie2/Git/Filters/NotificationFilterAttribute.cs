using Git.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Git.Filters
{
    public sealed class NotificationFilterAttribute : ActionFilterAttribute
    {
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var objectResult = context.Result as ObjectResult;

            if (objectResult?.Value is Notification notification && notification.Errors.Any())
            {
                context.Result = new BadRequestObjectResult(notification);
            }

            return base.OnResultExecutionAsync(context, next);
        }
    }
}
