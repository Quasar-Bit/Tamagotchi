using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TamagotchiWeb.Application.Animals.Base.DTOs
{
    public class GetAnimal
    {
        public int Id { get; set; }
        [DisplayName("Animal Id")]
        public int? AnimalId { get; set; }
        [Required(ErrorMessage = "Type field is required!")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "Name field is required!")]
        [DisplayName("Name")]
        public string? Name { get; set; }
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