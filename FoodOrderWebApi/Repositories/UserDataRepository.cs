using FoodOrderWebApi.Configuration;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;

namespace FoodOrderWebApi.Repositories;

public class UserDataRepository : IUserDataRepository
{
    private readonly FoodOrderDbContext _context;

    public UserDataRepository(FoodOrderDbContext context)
    {
        _context = context;
    }

    public UserData? GetUserData(string userId)
    {
        return _context.UserData
            .FirstOrDefault(userData => userData.UserId == userId);
    }

    public void CreateUserData(UserData userData)
    {
        _context.UserData.Add(userData);
        _context.SaveChanges();
    }

    public void UpdateUserData(UserData userData)
    {
        _context.UserData.Update(userData);
        _context.SaveChanges();
    }
}