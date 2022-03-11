using TamagotchiWeb.Services.DTOs.OutPut.Common;

namespace TamagotchiWeb.Services.DTOs.OutPut
{
    public class OrganizationsDto
    {
        public List<Organization> organizations { get; set; }
        public Pagination pagination { get; set; }
    }

    public class Hours
    {
        public string monday { get; set; }
        public string tuesday { get; set; }
        public string wednesday { get; set; }
        public string thursday { get; set; }
        public string friday { get; set; }
        public string saturday { get; set; }
        public object sunday { get; set; }
    }

    public class Adoption
    {
        public string policy { get; set; }
        public string url { get; set; }
    }

    public class SocialMedia
    {
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string youtube { get; set; }
        public string instagram { get; set; }
        public object pinterest { get; set; }
    }

    public class Organization
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Address address { get; set; }
        public Hours hours { get; set; }
        public string url { get; set; }
        public string website { get; set; }
        public string mission_statement { get; set; }
        public Adoption adoption { get; set; }
        public SocialMedia social_media { get; set; }
        public List<Photo> photos { get; set; }
        public object distance { get; set; }
        public Links _links { get; set; }
    }
}