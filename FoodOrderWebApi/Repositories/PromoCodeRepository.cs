using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repositories;

public class PromoCodeRepository : IRepository<PromoCode, int>
{
    private readonly FoodOrderDbContext _context;

    public PromoCodeRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public List<PromoCode> GetAll()
    {
        return _context.PromoCodes
            .AsNoTracking()
            .ToList();
    }

    public PromoCode? GetByIdOrName(int key)
    {
        return _context.PromoCodes
            .Where(code => code.Id == key)
            .AsNoTracking()
            .FirstOrDefault();
    }
}