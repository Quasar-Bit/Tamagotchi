namespace TamagotchiWeb.Entities.Interface
{
    public interface IHasKey<out T>
    {
        T DbId { get; }
    }
}