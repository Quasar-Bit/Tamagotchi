using Tamagotchi.Data.Repositories.Base;
using Tamagotchi.Data.Repositories.Interfaces;
using Tamagotchi.Data.Entities;

namespace Tamagotchi.Data.Repositories
{
    public class AnimalTypeRepository : BaseRepository<AnimalType>, IAnimalTypeRepository
    {
        public AnimalTypeRepository(Context context) : base(context)
        {
        }
    }
}