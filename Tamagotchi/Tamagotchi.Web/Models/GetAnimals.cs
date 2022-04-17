using Tamagotchi.Data.Entities;
using Tamagotchi.Web.Services.DTOs.OutPut.Common;

namespace Tamagotchi.Web.Models
{
    public class GetAnimals
    {
        public IEnumerable<Animal> Animals { get; set; }
        public Pagination Pagination { get; set; }
    }
}