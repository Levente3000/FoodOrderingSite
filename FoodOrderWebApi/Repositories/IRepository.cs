namespace FoodOrderWebApi.Repositories;

public interface IRepository<TEntity, in TKeyType> where TEntity : class
{
    public List<TEntity> GetAll();
    public TEntity? GetByIdOrName(TKeyType key);
}