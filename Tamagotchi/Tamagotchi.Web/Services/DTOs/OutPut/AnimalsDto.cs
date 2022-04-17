using Tamagotchi.Web.Services.DTOs.OutPut.Common;

namespace Tamagotchi.Web.Services.DTOs.OutPut
{
    public class AnimalsDto
    {
        public List<Animal> animals { get; set; }
        public Pagination pagination { get; set; }
    }

    public class Breeds
    {
        public string primary { get; set; }
        public string secondary { get; set; }
        public bool mixed { get; set; }
        public bool unknown { get; set; }
    }

    public class Colors
    {
        public string primary { get; set; }
        public string secondary { get; set; }
        public string tertiary { get; set; }
    }

    public class Attributes
    {
        public bool spayed_neutered { get; set; }
        public bool house_trained { get; set; }
        public bool? declawed { get; set; }
        public bool special_needs { get; set; }
        public bool shots_current { get; set; }
    }

    public class Environment
    {
        public bool? children { get; set; }
        public bool? dogs { get; set; }
        public bool? cats { get; set; }
    }

    public class Contact
    {
        public string email { get; set; }
        public string phone { get; set; }
        public Address address { get; set; }
    }

    public class Animal
    {
        public int id { get; set; }
        public string organization_id { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public string species { get; set; }
        public Breeds breeds { get; set; }
        public Colors colors { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string size { get; set; }
        public string coat { get; set; }
        public Attributes attributes { get; set; }
        public Environment environment { get; set; }
        public List<string> tags { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string organization_animal_id { get; set; }
        public List<Photo> photos { get; set; }
        public Photo primary_photo_cropped { get; set; }
        public List<object> videos { get; set; }
        public string status { get; set; }
        public DateTime status_changed_at { get; set; }
        public DateTime published_at { get; set; }
        public object distance { get; set; }
        public Contact contact { get; set; }
        public Links _links { get; set; }
    }
}