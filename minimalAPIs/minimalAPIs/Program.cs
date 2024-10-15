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

app.MapGet("/products", async (ProductsDb db) =>
{
    var products = await db.Products.ToListAsync();
    return Results.Ok(products);
});

app.MapGet("/products/{id:int}", async (ProductsDb db, int id) =>
{
    var product = await db.Products.FindAsync(id);
    return Results.Ok(product);
});

app.MapGet("/search/{name}", async (string name, ProductsDb db) =>
{
    var products = await db.Products.Where(p=>p.Name.Contains(name)).ToListAsync();
    return Results.Ok(products);
});

app.MapPost("/products", async (ProductsDb db, [FromBody]Product product) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});


app.UseHttpsRedirection();
app.Run();

