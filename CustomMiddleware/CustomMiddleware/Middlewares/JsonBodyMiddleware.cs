




using System.Text;

namespace CustomMiddleware.Middlewares
{
    /// <summary>
    /// Eğer request post ya da put ise VE JSON body's varsa bir sonraki middleware'a göndermek üzere bu JSON datasını izole eder.
    /// </summary>
    public class JsonBodyMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonBodyMiddleware(RequestDelegate next)
        {
            //bir standart: Bir sonraki middleware'a httpRequest nesnesini iletmek için "next" isminde RequestDelegate kullanıyoruz.
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (isAvailableForFilter(context))
            {
                string jsonRequestBody =  await getJsonFromBody(context);
                if (!string.IsNullOrEmpty(jsonRequestBody))
                {
                    replaceRequestBodyWithJsonStream(context, jsonRequestBody);
                    saveJsonToContextItem(context, jsonRequestBody);
                     
                }
            }

            await _next(context);
        }

    

        private bool isAvailableForFilter(HttpContext context)
        {
            return (isPostRequest(context) || isPutRequest(context)) && isJsonRequest(context);
        }

        private bool isJsonRequest(HttpContext context)
        {
            return context.Request.ContentType.StartsWith("application/json");

        }

        private bool isPutRequest(HttpContext context)
        {
            return context.Request.Method == HttpMethods.PUT;
        }

        private bool isPostRequest(HttpContext context)
        {
            return context.Request.Method == HttpMethods.POST;

        }

        private async Task<string> getJsonFromBody(HttpContext context)
        {
            using var reader = new StreamReader(context.Request.Body);
            return await reader.ReadToEndAsync();
        }

        private void replaceRequestBodyWithJsonStream(HttpContext context, string jsonRequestBody)
        {
            var content = Encoding.UTF8.GetBytes(jsonRequestBody);
            var requestBodyStream  =new MemoryStream();
            requestBodyStream.Write(content, 0, content.Length);

            context.Request.Body = requestBodyStream;
            context.Request.Body.Seek(0, SeekOrigin.Begin);

        }
        private void saveJsonToContextItem(HttpContext context, string jsonRequestBody)
        {
            //DİKKAT!!!
            //http isteği üzerinde bulunan JSON Stream'i eski hale getirdiyseniz, kopyasını nasıl başka bir middleware'a nasıl göndereceksiniz?

            context.Items["jsonBody"] = jsonRequestBody;
        }
    }

    class HttpMethods
    {
        public const string POST = "POST";
        public const string PUT = "PUT";
    }
}
