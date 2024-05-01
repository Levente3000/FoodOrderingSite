using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IUserDataRepository
{
    public UserData? GetUserData(string userId);
}