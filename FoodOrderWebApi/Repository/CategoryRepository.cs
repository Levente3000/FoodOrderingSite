using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderWebApi.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly FoodOrderDbContext _context;

        public CategoryRepository(FoodOrderDbContext context, IRepository<Category> repository)
        {
            _context = context;
            
        }

        public List<Category> GetAll()
        {
            return _context.Categories
                .AsNoTracking()
                .ToList();
        }

        public Category? GetById(object key)
        {
            return _context.Categories
                .Where(c => c.Name == key)
                .AsNoTracking()
                .SingleOrDefault();
        }
    }
}
