using Tamagotchi.Data.Entities;
using Tamagotchi.Data.Repositories.Base;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Data.Repositories
{
    public class AppSettingRepository : BaseRepository<AppSetting>, IAppSettingRepository
    {
        public AppSettingRepository(Context context) : base(context)
        {
        }
    }
}