using deploymentWithDocker.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbServer = builder.Configuration.GetValue<string>("DBServer");
var pass = builder.Configuration.GetValue<string>("DBPass");
var connectionString = builder.Configuration.GetConnectionString("db");
connectionString = connectionString.Replace("[HOST]", dbServer);
connectionString = connectionString.Replace("[PASS]", pass);

builder.Services.AddDbContext<SampleDbContext>(option => option.UseSqlServer(connectionString));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db =  scope.ServiceProvider.GetRequiredService<SampleDbContext>();
db.Database.Migrate();
app.Logger.LogInformation($"Bağlantı adresi: {connectionString}");
app.Logger.LogInformation($"Veritabanı oluşturuldu!");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/customers", async (SampleDbContext dbContext) =>
{
    var customers = await dbContext.Customers.ToListAsync();
    return Results.Ok(customers);
})
.WithName("customers")
.WithOpenApi();

app.Run();


