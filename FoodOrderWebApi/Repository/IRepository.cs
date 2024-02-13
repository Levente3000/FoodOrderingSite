namespace FoodOrderWebApi.Repository
{
    public interface IRepository<Entity> where Entity : class
    {
        public List<Entity> GetAll();
        public Entity? GetByIdOrName(object key);
    }
}
