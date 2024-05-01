using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IUserDataService
{
    public UserData? GetUserData(string userId);
}