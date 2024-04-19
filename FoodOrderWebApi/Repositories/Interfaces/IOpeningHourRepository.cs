using FoodOrderWebApi.Models;

namespace FoodOrderWebApi.Repositories;

public interface IOpeningHourRepository : IRepository<OpeningHour, int>
{
    public void CreateOpeningHour(OpeningHour openingHour);

    public void RemoveOpeningHour(OpeningHour openingHour);
}