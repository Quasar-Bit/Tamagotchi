using TamagotchiWeb.Entities.Interface;

namespace TamagotchiWeb.Entities.Base
{
    public abstract class EntityBase<T> : IHasKey<T>
    {
        public T DbId { get; set; }
    }
}