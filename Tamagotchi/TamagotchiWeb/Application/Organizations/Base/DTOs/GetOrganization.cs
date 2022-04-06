using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TamagotchiWeb.Application.Organizations.Base.DTOs
{
    public class GetOrganization
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Organization Id field is required!")]
        [DisplayName("Organization Id")]
        public string OrganizationId { get; set; }

        [Required(ErrorMessage = "Name field is required!")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        [DisplayName("Address")]
        public string Address1 { get; set; }

        //public string url { get; set; }
        //public string photos { get; set; }
        //public string primaryPhoto { get; set; }
        //public string primaryIcon { get; set; }
        //public string address2 { get; set; }
        //public string city { get; set; }
        //public string state { get; set; }
        //public string postcode { get; set; }
        //public string country { get; set; }
    }
}