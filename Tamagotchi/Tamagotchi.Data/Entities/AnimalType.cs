using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.Data.Entities
{
    public class AnimalType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Coats { get; set; }
        public string? Colors { get; set; }
        public string? Genders { get; set; }
    }
}