using eshop.Catalog.Application.Contracts;
using eshop.Catalog.Application.Extensions;
using eshop.Catalog.Application.Features.Products.CreateNewProduct;
using eshop.Catalog.Application.Features.Products.GetProducts;
using eshop.Catalog.Infrastructure.Data;
using eshop.Catalog.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<CatalogDbContext>(option => option.UseSqlServer(connectionString));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddApplicationService();

builder.Services.AddCors(option => option.AddPolicy("allow", builder =>
{
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.AllowAnyOrigin();
    /*
       http://www.x.com
       https://post.x.com:8181
       
     */
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/products", async (IMediator mediator) =>
{
    var response = await mediator.Send(new GetProductsRequestQuery());
    return Results.Ok(response);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/products", async (IMediator mediator, CreateProductRequest request) =>
{

    var response = await mediator.Send(request);
    return Results.Created($"/products/{response.Id}", null);

});

app.UseCors("allow");

app.Run();
