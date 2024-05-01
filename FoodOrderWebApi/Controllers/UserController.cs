using System.Security.Claims;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Services;
using FoodOrderWebApi.Services.Interfaces;
using Keycloak.Net.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Controllers;

[Route("user")]
[ApiController]
public class UserController : Controller
{
    private readonly KeycloakService _keycloakService;
    private readonly IUserDataService _userDataService;

    public UserController(KeycloakService keycloakService, IUserDataService userDataService)
    {
        _keycloakService = keycloakService;
        _userDataService = userDataService;
    }

    [HttpGet]
    public async Task<List<User>> GetUsersAsync()
    {
        return await _keycloakService.GetUsersAsync();
    }

    [HttpGet("user-data")]
    public UserData? GetUserData()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return userId.IsNullOrEmpty() ? null : _userDataService.GetUserData(userId);
    }
}