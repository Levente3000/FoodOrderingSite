using Keycloak.Net;
using Keycloak.Net.Models.Roles;
using Keycloak.Net.Models.Users;

namespace FoodOrderWebApi.Services;

public class KeycloakService
{
    private readonly KeycloakClient _adminClient = new(
        "http://localhost:8090",
        "user",
        "admin",
        new KeycloakOptions(
            adminClientId: "admin-cli",
            authenticationRealm: "master"
        )
    );

    private const string REALM = "food-ordering-site";
    private const string CLIENTID = "food-ordering-site";

    public async Task<List<User>> GetUsersAsync()
    {
        return (await _adminClient.GetUsersAsync(REALM)).ToList();
    }

    public async Task<User> GetUserAsync(string userId)
    {
        return await _adminClient.GetUserAsync(REALM, userId);
    }

    public async Task AssignRestaurantOwnerRoleToUserAsync(string userId)
    {
        const string roleName = "RESTAURANT_OWNER";
        var roles = await _adminClient.GetRolesAsync(REALM);
        var role = roles.FirstOrDefault(r => r.Name == roleName);

        if (role == null)
        {
            throw new Exception($"Role {roleName} not found");
        }

        var result = await _adminClient.AddRealmRoleMappingsToUserAsync(REALM, userId, new List<Role>
        {
            new Role { Id = role.Id, Name = role.Name }
        });

        if (!result)
        {
            throw new Exception($"Failed to assign role {roleName} to user {userId}");
        }
    }
}