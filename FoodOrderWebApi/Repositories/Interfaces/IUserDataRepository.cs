using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories.Interfaces;

public interface IUserDataRepository
{
    public UserData? GetUserData(string userId);

    public void CreateUserData(UserData userData);

    public void UpdateUserData(UserData userData);
}