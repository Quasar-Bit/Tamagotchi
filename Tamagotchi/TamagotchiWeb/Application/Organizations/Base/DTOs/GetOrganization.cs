using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TamagotchiWeb.Application.Organizations.Base.DTOs
{
    public class GetOrganization
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Organization field is required!")]
        [DisplayName("Organization Id")]
        public string organizationId { get; set; }

        [Required(ErrorMessage = "Name field is required!")]
        [DisplayName("Name")]
        public string name { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        [DisplayName("Phone")]
        public string phone { get; set; }

        [DisplayName("Website")]
        public string website { get; set; }

        [DisplayName("Address")]
        public string address1 { get; set; }

        public string url { get; set; }
        public string photos { get; set; }
        public string primaryPhoto { get; set; }
        public string primaryIcon { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
    }
}