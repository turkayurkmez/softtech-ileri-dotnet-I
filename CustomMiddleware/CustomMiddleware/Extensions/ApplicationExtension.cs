using CustomMiddleware.Middlewares;

namespace CustomMiddleware.Extensions
{
    public static class ApplicationExtension
    {
        public static IApplicationBuilder UseBadWordsHandler(this IApplicationBuilder app) {

            app.UseMiddleware<JsonBodyMiddleware>();
            app.UseMiddleware<BadWordsHandlerMiddleware>();

            return app;

        }
    }
}
