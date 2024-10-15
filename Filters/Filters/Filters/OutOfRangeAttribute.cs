using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Filters.Filters
{
    public class OutOfRangeAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentOutOfRangeException)
            {
                string message = $"Uygulamada hata meydana geldi.\nHata mesajı: { context.Exception.Message}\n Hatanın meydana geldiği fonksiyon: {context.ActionDescriptor.DisplayName}";

                context.Result = new ViewResult()
                {
                    ViewName = "Exception",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = message
                    }

                };
            }
        }
    }
}
