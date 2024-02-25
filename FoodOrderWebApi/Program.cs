using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Model;
using FoodOrderWebApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRepository<Restaurant, int>, RestaurantRepository>();
builder.Services.AddScoped<IRepository<FoodCategory, string>, FoodCategoryRepository>();

builder.Services.AddDbContext<FoodOrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.UseNodaTime()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var serviceScope = app.Services.CreateScope())
    {
        DbInitializer.Initialize(serviceScope.ServiceProvider);
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
    .WithOrigins("http://localhost",
        "http://localhost:4200",
        "https://localhost:7233")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseStaticFiles();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
