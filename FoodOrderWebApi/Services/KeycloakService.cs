﻿using Keycloak.Net;
using Keycloak.Net.Models.Roles;
using Keycloak.Net.Models.Users;

namespace FoodOrderWebApi.Services;

public class KeycloakService
{
    private KeycloakClient _adminClient = new(
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
}