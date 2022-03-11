using TamagotchiWeb.Models;

namespace TamagotchiWeb.Services.Interfaces
{
    public interface IAnimalTypeService
    {
        Task<GetAnimalTypes> GetAnimalTypes();
    }
}