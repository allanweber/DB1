using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RH.Domain.CommandHandlers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
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
