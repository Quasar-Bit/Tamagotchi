using Tamagotchi.Data.Entities;
using Tamagotchi.Web.Services.DTOs.OutPut.Common;

namespace Tamagotchi.Web.Models
{
    public class GetOrganizations
    {
        public IEnumerable<Organization> Organizations { get; set; }
        public Pagination Pagination { get; set; }
    }
}