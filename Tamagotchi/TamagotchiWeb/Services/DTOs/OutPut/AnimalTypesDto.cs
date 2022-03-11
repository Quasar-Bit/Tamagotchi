using TamagotchiWeb.Services.DTOs.OutPut.Common;

namespace TamagotchiWeb.Services.DTOs.OutPut
{
    public class AnimalTypesDto
    {
        public List<Type> types { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public List<string> coats { get; set; }
        public List<string> colors { get; set; }
        public List<string> genders { get; set; }
        public Links _links { get; set; }
    }
}