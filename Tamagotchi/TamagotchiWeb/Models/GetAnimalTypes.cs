using Tamagotchi.Data.Entities;

namespace TamagotchiWeb.Models
{
    public class GetAnimalTypes
    {
        public IEnumerable<AnimalType> AnimalTypes { get; set; }
    }
}