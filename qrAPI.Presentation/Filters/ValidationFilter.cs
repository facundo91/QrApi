﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using qrAPI.Contracts.v1.Responses;

namespace qrAPI.Presentation.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();
                var errorResponse = new ErrorResponse();
                foreach (var (key, value) in errorsInModelState)
                {
                    foreach (var subError in value)
                    {
                        var errorModel = new ErrorModel
                        {
                            FieldName = key,
                            Message = subError
                        };
                        errorResponse.Errors.Add(errorModel);
                    }
                }
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();

            // after controller
        }
    }
}