using AutoMapper;
using FoodOrderWebApi.Models;
using FoodOrderWebApi.Repositories.Interfaces;
using FoodOrderWebApi.Services.Interfaces;

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
        return _userDataRepository.GetUserData(userId);
    }
}