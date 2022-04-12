using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tamagotchi.Application.Animals.Base.DTOs
{
    public class GetAnimal
    {
        public int Id { get; set; }
        [DisplayName("Id")]
        public int? AnimalId { get; set; }
        [Required(ErrorMessage = "Type field is required!")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Name field is required!")]
        public string Name { get; set; }
        [DisplayName("Breed")]
        public string? PrimaryBreed { get; set; }
        public string? Gender { get; set; }
        public string? Age { get; set; }
        [DisplayName("Color")]
        public string? PrimaryColor { get; set; }
        [DisplayName("Organization Id")]
        public string? OrganizationId { get; set; }

        //public string Size { get; set; }
        //public bool SpayedNeutered { get; set; }
        //public bool Declawed { get; set; }
        //public string Tags { get; set; }
        //public string Description { get; set; }
        //public string OrganizationAnimalId { get; set; }
        //public string Photos { get; set; }
        //public string PrimaryPhoto { get; set; }
        //public string PrimaryIcon { get; set; }
        //public DateTime? Published_at { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
    }
}