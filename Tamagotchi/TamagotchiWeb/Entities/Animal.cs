namespace TamagotchiWeb.Entities
{
    public class Animal
    {
        public int Id { get; set; }
        public int? AnimalId { get; set; }
        public string? OrganizationId { get; set; }
        public string? Url { get; set; }
        public string? Type { get; set; }
        public string? Species { get; set; }
        public string? PrimaryBreed { get; set; }
        public string? SecondaryBreed { get; set; }
        public bool? IsMixedBreed { get; set; }
        public bool? IsUnknownBreed { get; set; }
        public string? PrimaryColor { get; set; }
        public string? SecondaryColor { get; set; }
        public string? TertiaryColor { get; set; }
        public string? Age { get; set; }
        public string? Gender { get; set; }
        public string? Size { get; set; }
        public string? Coat { get; set; }
        public bool? SpayedNeutered { get; set; }
        public bool? HouseTrained { get; set; }
        public bool? Declawed { get; set; }
        public bool? SpecialNeeds { get; set; }
        public bool? ShotsCurrent { get; set; }
        public bool? ChildrenEnvinronment { get; set; }
        public bool? DogsEnvinronment { get; set; }
        public bool? CatsEnvinronment { get; set; }
        public string? Tags { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? OrganizationAnimalId { get; set; }
        public string? Photos { get; set; }
        public string? PrimaryPhoto { get; set; }
        public string? PrimaryIcon { get; set; }
        public string? Videos { get; set; }
        public string? Status { get; set; }
        public DateTime? Status_changed_at { get; set; }
        public DateTime? Published_at { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Postcode { get; set; }
        public string? Country { get; set; }
    }
}