using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderWebApi.Controllers;

public class MyController : Controller
{
    public string? GetUserId()
    {
        return HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}