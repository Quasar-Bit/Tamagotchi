using TamagotchiWeb.Data.Repositories.Base;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Entities;

namespace TamagotchiWeb.Data.Repositories
{
    public class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(Context context) : base(context)
        {
        }
    }
}