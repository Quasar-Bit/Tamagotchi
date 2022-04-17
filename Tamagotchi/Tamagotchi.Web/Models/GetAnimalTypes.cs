using Tamagotchi.Data.Entities;

namespace Tamagotchi.Web.Models
{
    public class GetAnimalTypes
    {
        public IEnumerable<AnimalType> AnimalTypes { get; set; }
    }
}