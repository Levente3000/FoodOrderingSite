using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class OpeningHourRepository : IOpeningHourRepository
{
    private readonly FoodOrderDbContext _context;

    public OpeningHourRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public List<OpeningHour> GetAll()
    {
        return _context.OpeningHours
            .AsNoTracking()
            .ToList();
    }

    public OpeningHour? GetByIdOrName(int key)
    {
        return _context.OpeningHours
            .Where(oh => oh.Id == key)
            .AsNoTracking()
            .FirstOrDefault();
    }

    public void CreateOpeningHour(OpeningHour openingHour)
    {
        _context.OpeningHours.Add(openingHour);
        _context.SaveChanges();
    }

    public void RemoveOpeningHour(OpeningHour openingHour)
    {
        _context.OpeningHours.Remove(openingHour);
        _context.SaveChanges();
    }
}