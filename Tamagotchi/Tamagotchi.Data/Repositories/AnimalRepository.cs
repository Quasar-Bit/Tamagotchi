using Tamagotchi.Data.Repositories.Base;
using Tamagotchi.Data.Repositories.Interfaces;
using Tamagotchi.Data.Entities;

namespace Tamagotchi.Data.Repositories
{
    public class AnimalRepository : BaseRepository<Animal>, IAnimalRepository
    {
        public AnimalRepository(Context context) : base(context)
        {
        }
    }
}