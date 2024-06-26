using System.Security.Claims;
using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services;
using FoodOrderWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IFoodCategoryRepository, FoodCategoryRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IRepository<PromoCode, int>, PromoCodeRepository>();
builder.Services.AddScoped<IOpeningHourRepository, OpeningHourRepository>();
builder.Services.AddScoped<IRestaurantPermissionRepository, RestaurantPermissionRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserDataRepository, UserDataRepository>();
builder.Services.AddScoped<IFavouriteRestaurantRepository, FavouriteRestaurantRepository>();

builder.Services.AddScoped<IAssetsService, AssetsService>();
builder.Services.AddScoped<IKeycloakService, KeycloakService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IRestaurantPermissionService, RestaurantPermissionService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserDataService, UserDataService>();
builder.Services.AddScoped<IFavouriteRestaurantService, FavouriteRestaurantService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<FoodOrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.UseNodaTime()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// KeyCloak
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = builder.Configuration["Jwt:Authority"];
    o.Audience = builder.Configuration["Jwt:Audience"];
    o.RequireHttpsMetadata = bool.Parse(builder.Configuration["Jwt:RequireHttpsMetadata"]);
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        RoleClaimType = ClaimTypes.Role,
    };
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();