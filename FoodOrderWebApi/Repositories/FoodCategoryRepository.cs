using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class FoodCategoryRepository : IRepository<FoodCategory, string>
{
    private readonly FoodOrderDbContext _context;

    public FoodCategoryRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public List<FoodCategory> GetAll()
    {
        return _context.FoodCategories
            .AsNoTracking()
            .ToList();
    }

    public FoodCategory? GetByIdOrName(string key)
    {
        return _context.FoodCategories
            .Where(c => c.Name == key)
            .AsNoTracking()
            .SingleOrDefault();
    }
}