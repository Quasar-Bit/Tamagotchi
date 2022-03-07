namespace TamagotchiWeb.Entities.Base
{
    public class Entity : EntityBase<string>
    {
        protected Entity()
        {
            DbId = Guid.NewGuid().ToString();
        }
    }
}