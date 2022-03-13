using TamagotchiWeb.Data.Repositories.Base;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Entities;

namespace TamagotchiWeb.Data.Repositories
{
    public class AnimalTypeRepository : BaseRepository<AnimalType>, IAnimalTypeRepository
    {
        public AnimalTypeRepository(Context context) : base(context)
        {
        }
    }
}