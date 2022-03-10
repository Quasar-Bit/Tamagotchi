using TamagotchiWeb.Models;

namespace TamagotchiWeb.Services
{
    public interface IAnimalService
    {
        Task<GetAnimals> GetAnimals(int page);
    }
}