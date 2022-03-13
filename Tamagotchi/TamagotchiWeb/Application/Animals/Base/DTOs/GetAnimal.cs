namespace TamagotchiWeb.Application.Animals.Base.DTOs
{
    public class GetAnimal
    {
        public int AnimalId { get; set; }
        public string OrganizationId { get; set; }
        public string Type { get; set; }
        public string PrimaryBreed { get; set; }
        public string PrimaryColor { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Size { get; set; }
        public bool SpayedNeutered { get; set; }
        public bool Declawed { get; set; }
        public string Tags { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OrganizationAnimalId { get; set; }
        public string Photos { get; set; }
        public string PrimaryPhoto { get; set; }
        public string PrimaryIcon { get; set; }
        public DateTime? Published_at { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}