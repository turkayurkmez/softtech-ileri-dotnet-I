using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = "client.api.com",
        ValidIssuer = "server.api.com",
        ValidateIssuer = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-ifade-onay-icin-kritik-ve-önemli"))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder().AddPolicy("adminPolicy", authBuilder =>
{
    authBuilder.RequireRole("admin")
               .RequireClaim(JwtRegisteredClaimNames.Name);

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi()
.RequireAuthorization("adminPolicy");


app.MapPost("/login", (UserLoginModel model ) =>
{
    if (model.UserName == "user" && model.Password =="1234")
    {
        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-ifade-onay-icin-kritik-ve-önemli"));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        Claim[] claims = new[] {
            new Claim(JwtRegisteredClaimNames.Name, "Turkay"),
            new Claim(ClaimTypes.Role, "admin")
        };

        var token = new JwtSecurityToken(
            issuer: "server.api.com",
            audience: "client.api.com",
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
            );

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.WriteToken(token);

        return Results.Ok(jwtToken);
    }
    return Results.BadRequest(new { message = "Kullanıcı girişi hatalı" });
});

//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record UserLoginModel(string UserName, string Password);