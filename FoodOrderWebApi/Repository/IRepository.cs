namespace FoodOrderWebApi.Repository
{
    public interface IRepository<Entity, KeyType> where Entity : class
    {
        public List<Entity> GetAll();
        public Entity? GetByIdOrName(KeyType key);
    }
}
