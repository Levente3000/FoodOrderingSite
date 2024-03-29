using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRepository<Restaurant, int>, RestaurantRepository>();
builder.Services.AddScoped<IRepository<FoodCategory, string>, FoodCategoryRepository>();
builder.Services.AddScoped<AssetsService>();
builder.Services.AddScoped<RestaurantService>();

builder.Services.AddAutoMapper(typeof(Program));

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
app.UseCors(policyBuilder => policyBuilder
    .WithOrigins(
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