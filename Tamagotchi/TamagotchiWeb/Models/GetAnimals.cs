using TamagotchiWeb.Services.DTOs.OutPut;
using TamagotchiWeb.Services.DTOs.OutPut.Common;

namespace TamagotchiWeb.Models
{
    public class GetAnimals
    {
        public IEnumerable<Entities.Animal> Animals { get; set; }
        public Pagination Pagination { get; set; }
    }
}