using FoodOrderWebApi.Configuration;
using Microsoft.EntityFrameworkCore;

namespace UnitTest;

public class DbContextOptions
{
    public static DbContextOptions<FoodOrderDbContext> GetOptions(string databaseName)
    {
        return new DbContextOptionsBuilder<FoodOrderDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
    }
}