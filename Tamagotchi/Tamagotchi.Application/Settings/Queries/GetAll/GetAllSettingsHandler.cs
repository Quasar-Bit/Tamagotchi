using MapsterMapper;
using MediatR;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Settings.Base.DTOs;
using Tamagotchi.Application.Settings.Queries.GetAll.DTOs;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Settings.Queries.GetAll
{
    internal class GetAllSettingsHandler : BaseRequestHandler, IRequestHandler<GetAppSettingsQuery, IEnumerable<GetAppSetting>>
    {
        private readonly IAppSettingRepository _appSettingRepository;

        public GetAllSettingsHandler(
            IAppSettingRepository appSettingRepository,
            IMapper mapper) : base(mapper)
        {
            _appSettingRepository = appSettingRepository;
        }

        public async Task<IEnumerable<GetAppSetting>> Handle(GetAppSettingsQuery request,
            CancellationToken cancellationToken)
        {
            return _appSettingRepository.GetReadOnlyQuery().Select(Mapper.Map<GetAppSetting>);
        }
    }
}