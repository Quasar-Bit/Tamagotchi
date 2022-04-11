using Tamagotchi.Data.Entities;
using TamagotchiWeb.Services.DTOs.OutPut.Common;

namespace TamagotchiWeb.Models
{
    public class GetAnimals
    {
        public IEnumerable<Animal> Animals { get; set; }
        public Pagination Pagination { get; set; }
    }
}