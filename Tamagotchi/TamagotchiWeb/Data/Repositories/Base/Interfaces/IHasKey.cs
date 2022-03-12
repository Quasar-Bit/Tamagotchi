namespace TamagotchiWeb.Data.Repositories.Base.Interfaces
{
    public interface IHasKey<out T>
    {
        T Id { get; }
    }
}