using AutoMapper;
using FoodOrderWebApi.DTOs;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FoodOrderWebApi.Services;

public class UserDataService : IUserDataService
{
    private readonly IUserDataRepository _userDataRepository;
    private readonly KeycloakService _keycloakService;
    private readonly IMapper _mapper;

    public UserDataService(IUserDataRepository userDataRepository, KeycloakService keycloakService, IMapper mapper)
    {
        _userDataRepository = userDataRepository;
        _keycloakService = keycloakService;
        _mapper = mapper;
    }

    public UserData? GetUserData(string userId)
    {
        var userData = _userDataRepository.GetUserData(userId);
        if (userData == null)
        {
            var keyCloakUser = _keycloakService.GetUserAsync(userId).Result;
            userData = new UserData
            {
                Name = keyCloakUser.FirstName + " " + keyCloakUser.LastName,
                Email = keyCloakUser.Email
            };
        }

        return userData;
    }

    public bool GetUserHasData(string userId)
    {
        var userData = _userDataRepository.GetUserData(userId);
        return userData != null && !string.IsNullOrEmpty(userData?.Address) && !string.IsNullOrEmpty(userData?.Phone);
    }

    public void UpdateUserData(string userId, UpdateUserDataDto userData)
    {
        var userDataInDatabase = _userDataRepository.GetUserData(userId);

        if (userDataInDatabase == null)
        {
            _userDataRepository.CreateUserData(new UserData
            {
                UserId = userId,
                Name = userData.Name,
                Address = userData.Address,
                Email = userData.Email,
                Phone = userData.Phone
            });
        }
        else
        {
            _mapper.Map(userData, userDataInDatabase);

            _userDataRepository.UpdateUserData(userDataInDatabase);
        }
    }
}