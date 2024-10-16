namespace RouteHandlers
{
    public static class ProductEndpoints
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/products/all", () => Results.Ok(new { messagee = "Farz edin ki burada ürün listesi var" })
                      ).WithName("Products");

            app.MapGet("/links", (LinkGenerator generator) => $"Tüm ürünleri almak için gereken link: {generator.GetPathByName("Products", values: null)}");
                         
            app.MapPost("/", () => Results.Created("created/1", new { message = "Eklenen eleman...." }));
        }


    }
}
