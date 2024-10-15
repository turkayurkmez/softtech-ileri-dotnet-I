using Filters.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Filters.Filters
{
    public class StopwatchAttribute : ActionFilterAttribute
    {

        private Stopwatch stopwatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
           stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
           
            
            var viewResult = context.Result as ViewResult;
            var model = (ModelBase)viewResult.Model;
            if (model != null) {
                model.ExecutionTime = stopwatch.Elapsed.TotalMilliseconds;
            }
            else
            {
                viewResult.ViewData["Stopwatch"]=stopwatch.Elapsed.TotalMilliseconds;
            }

            stopwatch.Reset();

            
        }

    }
}
