using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TamagotchiWeb.Application.AnimalTypes.Base.DTOs
{
    public class GetAnimalType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Need")]
        public string? Name { get; set; }
        [DisplayName("Display Coats")]
        public string? Coats { get; set; }
        public string? Colors { get; set; }
        //[Range(1,100,ErrorMessage = "Enter number between 1 and 100 only!")]
        public string? Genders { get; set; }
    }
}