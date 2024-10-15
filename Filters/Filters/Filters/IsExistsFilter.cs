using Filters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters.Filters
{
    public class IsExistsFilter : IActionFilter
    {

        private readonly IProductService productService;
        private readonly ILogger<IsExistsFilter> logger;

        public IsExistsFilter(IProductService productService, ILogger<IsExistsFilter> logger)
        {
            this.productService = productService;
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation($"{context.ActionDescriptor.DisplayName} yürütme işlemi tamamlandı");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation($"{context.ActionDescriptor.DisplayName} yürütme işlemi başladı!");

            if (context.ActionArguments["id"] is int id)
            {
                if (!productService.IsExists(id))
                {
                    context.Result = new NotFoundResult();
                   
                }
            }
        }
    }
}
