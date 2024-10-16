using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minimalAPIs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductsDb>(opt => opt.UseInMemoryDatabase("Products"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var products = app.MapGroup("/products");

ProuctsOperations operations = new ProuctsOperations ();

products.MapGet("/", operations.GetAllProductsAsync );
products.MapGet("/{id:int}", operations.GetProductById);
products.MapGet("/search/{name}", operations.SearchByName);
products.MapPost("/", operations.CreateProduct);
products.MapPut("/{id:int}", operations.UpdateExisting);

products.MapDelete("/{id:int}", async (int id, ProductsDb db) =>
{
    if (await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id) is Product product)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.UseHttpsRedirection();
app.Run();



