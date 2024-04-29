using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

[Route("category")]
[ApiController]
public class CategoryController : Controller
{
    private readonly IRepository<FoodCategory, string> _categoryRepository;

    public CategoryController(IRepository<FoodCategory, string> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public List<FoodCategory> GetAllCategories()
    {
        return _categoryRepository.GetAll();
    }

    [HttpGet("names")]
    public List<string> GetAllCategoryNames()
    {
        return _categoryRepository.GetAll()
            .Select(category => category.Name)
            .ToList();
    }
}