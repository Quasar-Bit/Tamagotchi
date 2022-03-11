using TamagotchiWeb.Models;

namespace TamagotchiWeb.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<GetOrganizations> GetOrganizations(int page);
    }
}
