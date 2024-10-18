using CachingInMemory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddMemoryCache();
builder.Services.AddHostedService<CategoriesCacheBackgroundService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/categories", async (CategoryService categoryService) =>
{
    var categories = await categoryService.GetCategories(); 
    return Results.Ok(categories);
})
.WithName("categories")
.WithOpenApi();

app.Run();

