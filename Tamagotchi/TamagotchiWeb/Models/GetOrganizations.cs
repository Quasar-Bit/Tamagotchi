using TamagotchiWeb.Services.DTOs.OutPut.Common;

namespace TamagotchiWeb.Models
{
    public class GetOrganizations
    {
        public IEnumerable<Entities.Organization> Organizations { get; set; }
        public Pagination Pagination { get; set; }
    }
}