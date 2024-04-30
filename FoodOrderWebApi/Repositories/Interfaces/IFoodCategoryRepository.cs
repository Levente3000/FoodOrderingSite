using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IFoodCategoryRepository : IRepository<FoodCategory, string>
{
    public List<FoodCategory> GetCategoriesByNameList(List<string> categoryNames);
}