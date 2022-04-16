using Tamagotchi.Data.Entities;
using Tamagotchi.Data.Repositories.Base;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Data.Repositories
{
    public class AppSettingsRepository : BaseRepository<AppSetting>, IAppSettingsRepository
    {
        public AppSettingsRepository(Context context) : base(context)
        {
        }
    }
}