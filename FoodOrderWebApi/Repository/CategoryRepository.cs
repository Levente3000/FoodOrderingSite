using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly FoodOrderDbContext _context;

        public CategoryRepository(FoodOrderDbContext context)
        {
            _context = context;

        }

        public List<Category> GetAll()
        {
            return _context.Categories
                .AsNoTracking()
                .ToList();
        }

        public Category? GetByIdOrName(object key)
        {
            return _context.Categories
                .Where(c => c.Name == (string)key)
                .AsNoTracking()
                .SingleOrDefault();
        }
    }
}
