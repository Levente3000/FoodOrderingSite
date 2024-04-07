using Keycloak.Net;
using Keycloak.Net.Models.Roles;

namespace FoodOrderWebApi.Services;

public class KeycloakService
{
    private KeycloakClient _adminClient = new(
        "http://localhost:8090",
        "user",
        "admin",
        new KeycloakOptions(adminClientId: "admin-cli"
        )
    );

    private const string REALM = "food-ordering-site";
    private const string CLIENTID = "food-ordering-site";

    public void AddRoleToUser(string userId)
    {
    }
}