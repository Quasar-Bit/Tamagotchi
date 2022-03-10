namespace TamagotchiWeb.Entities
{
    public class Animal
    {
        public int id { get; set; }
        public int? animalId { get; set; }
        public string? organizationId { get; set; }
        public string? url { get; set; }
        public string? type { get; set; }
        public string? species { get; set; }
        public string? primaryBreed { get; set; }
        public string? secondaryBreed { get; set; }
        public bool? isMixedBreed { get; set; }
        public bool? isUnknownBreed { get; set; }
        public string? primaryColor { get; set; }
        public string? secondaryColor { get; set; }
        public string? tertiaryColor { get; set; }
        public string? age { get; set; }
        public string? gender { get; set; }
        public string? size { get; set; }
        public string? coat { get; set; }
        public bool? spayedNeutered { get; set; }
        public bool? houseTrained { get; set; }
        public bool? declawed { get; set; }
        public bool? specialNeeds { get; set; }
        public bool? shotsCurrent { get; set; }
        public bool? childrenEnvinronment { get; set; }
        public bool? dogsEnvinronment { get; set; }
        public bool? catsEnvinronment { get; set; }
        public string? tags { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? organizationAnimalId { get; set; }
        public string? photos { get; set; }
        public string? primaryPhoto { get; set; }
        public string? primaryIcon { get; set; }
        public string? videos { get; set; }
        public string? status { get; set; }
        public DateTime? status_changed_at { get; set; }
        public DateTime? published_at { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address1 { get; set; }
        public string? address2 { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postcode { get; set; }
        public string? country { get; set; }
    }
}