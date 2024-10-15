using CustomMiddleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
 * Bu API'ye gelen POST ya da PUT isteklerinde JSON verisi olup olmadığını analiz et.  --> JSONBodyMiddleware
 * JSON verisi içinde küfür gibi istenmeyen kelimeler varsa --> BadWordsHandlerMiddleware
 * İstemciye 400 döndür.
 */

app.UseHttpsRedirection();

app.UseMiddleware<JsonBodyMiddleware>();
app.UseMiddleware<BadWordsHandlerMiddleware>();

app.MapControllers();





app.Run();


