using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IUserDataService
{
    public UserData? GetUserData(string userId);

    public bool GetUserHasData(string userId);

    public void UpdateUserData(string userId, UpdateUserDataDto userData);
}