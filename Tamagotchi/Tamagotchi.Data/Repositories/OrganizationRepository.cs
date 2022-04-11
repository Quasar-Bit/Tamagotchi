using Tamagotchi.Data.Repositories.Base;
using Tamagotchi.Data.Repositories.Interfaces;
using Tamagotchi.Data.Entities;

namespace Tamagotchi.Data.Repositories
{
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(Context context) : base(context)
        {
        }
    }
}