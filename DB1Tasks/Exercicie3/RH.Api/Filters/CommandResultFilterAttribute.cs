using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RH.Domain.Core.CommandHandlers;
using System.Threading.Tasks;

namespace RH.Api.Filters
{
    public sealed class CommandResultFilterAttribute : ActionFilterAttribute
    {
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var objectResult = context.Result as ObjectResult;

            if (objectResult?.Value is FailureResult result && ((FailureResult)objectResult?.Value).IsFailure )
            {
                context.Result = new BadRequestObjectResult(result);
            }

            return base.OnResultExecutionAsync(context, next);

        }
    }
}
