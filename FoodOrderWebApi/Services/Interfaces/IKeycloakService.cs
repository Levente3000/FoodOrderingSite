using Keycloak.Net.Models.Users;

namespace FoodOrderWebApi.Services.Interfaces;

public interface IKeycloakService
{
    public Task<List<User>> GetUsersAsync();

    public Task<User> GetUserAsync(string userId);

    public Task AssignRestaurantOwnerRoleToUserAsync(string userId);
}