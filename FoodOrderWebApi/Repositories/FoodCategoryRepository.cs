using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class FoodCategoryRepository : IFoodCategoryRepository
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

    public List<FoodCategory> GetCategoriesByNameList(List<string> categoryNames)
    {
        return _context.FoodCategories
            .Where(c => categoryNames.Contains(c.Name))
            .ToList();
    }
}