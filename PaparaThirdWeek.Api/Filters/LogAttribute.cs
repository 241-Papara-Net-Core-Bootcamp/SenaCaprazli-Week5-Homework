using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Api.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public LogAttribute()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Trace.WriteLine($"Action method {context.ActionDescriptor.DisplayName} " +
                $"executing at {DateTime.Now.ToShortDateString()}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Trace.WriteLine($"Action method {context.ActionDescriptor.DisplayName}" +
                $" executed at {DateTime.Now.ToShortDateString()}");
        }

    }
}
