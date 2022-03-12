using TamagotchiWeb.Data.Repositories.Base;
using TamagotchiWeb.Data.Repositories.Interfaces;
using TamagotchiWeb.Entities;

namespace TamagotchiWeb.Data.Repositories
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(Context context) : base(context)
        {
        }
    }
}