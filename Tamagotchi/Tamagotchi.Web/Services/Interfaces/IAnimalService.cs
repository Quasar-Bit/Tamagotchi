using Tamagotchi.Web.Models;

namespace Tamagotchi.Web.Services
{
    public interface IAnimalService
    {
        Task<GetAnimals> GetAnimals(int page);
    }
}