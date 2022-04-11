using Tamagotchi.Data.Entities;
using TamagotchiWeb.Services.DTOs.OutPut.Common;

namespace TamagotchiWeb.Models
{
    public class GetOrganizations
    {
        public IEnumerable<Organization> Organizations { get; set; }
        public Pagination Pagination { get; set; }
    }
}