
namespace CustomMiddleware.Middlewares
{
    /// <summary>
    /// İstenmeyen sözler varsa bunları filtreleyen mdidleware
    /// </summary>
    public class BadWordsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public BadWordsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Items.TryGetValue("jsonBody", out object? jsonBody))
            {
                var badWords = new List<string>() { "çirkin", "nefret", "kötü" };
                var jsonBodyString = (string)(jsonBody);
                if (badWords.Any(jsonBodyString.Contains))
                {
                    await responseBadRequest(context);
                }
            }

          await  _next(context);
        }

        private async Task responseBadRequest(HttpContext context)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "applicaion/json";
            await context.Response.WriteAsJsonAsync(new { message = "Bu gönderide istenmeyen kelimeler var" });

        }
    }
}
