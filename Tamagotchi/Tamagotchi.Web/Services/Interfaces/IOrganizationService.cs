using Tamagotchi.Web.Models;

namespace Tamagotchi.Web.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<GetOrganizations> GetOrganizations(int page);
    }
}
