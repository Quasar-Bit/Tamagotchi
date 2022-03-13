namespace TamagotchiWeb.Application.Animals.Base.DTOs
{
    public class GetOrganization
    {
        public int id { get; set; }
        public string? organizationId { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? url { get; set; }
        public string? website { get; set; }
        public string? photos { get; set; }
        public string? primaryPhoto { get; set; }
        public string? primaryIcon { get; set; }
        public string? address1 { get; set; }
        public string? address2 { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postcode { get; set; }
        public string? country { get; set; }
    }
}
