using Tamagotchi.Web.Models;

namespace Tamagotchi.Web.Services.Interfaces
{
    public interface IAnimalTypeService
    {
        Task<GetAnimalTypes> GetAnimalTypes();
    }
}