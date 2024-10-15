using DIDetails.POC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//IoC: Injection of Container
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ISingleton, Singleton>();
builder.Services.AddTransient<ITransient, Transient>();
builder.Services.AddScoped<IScoped, Scoped>();
builder.Services.AddTransient<LifeTimeService>();


builder.Services.Add(ServiceDescriptor.Singleton<Singleton>(srv =>
{
    var lifeTime = srv.GetRequiredService<Singleton>();
    
    return lifeTime;
}));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var scopedInstance = scope.ServiceProvider.GetRequiredService<IScoped>();
app.Logger.LogInformation($"Program.cs'den okunan scope guid değeri: {scopedInstance.Guid.ToString()}");



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
