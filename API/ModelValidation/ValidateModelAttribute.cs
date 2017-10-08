using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace gameapi.ModelValidation
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        // private ILogger<ValidateModelAttribute> _logger;
        // public ValidateModelAttribute(ILogger<ValidateModelAttribute> logger)
        // {
        //     _logger = logger;
        // }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid == false)
            {
                // _logger.LogInformation("Client sent an illegal model");
                context.Result = new BadRequestObjectResult(context.ModelState);
            } 
        }
    }
}
