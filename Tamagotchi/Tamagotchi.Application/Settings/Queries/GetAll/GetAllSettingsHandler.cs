using MapsterMapper;
using MediatR;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Settings.Base.DTOs;
using Tamagotchi.Application.Settings.Queries.GetAll.DTOs;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Settings.Queries.GetAll
{
    internal class GetAppSettingsHandler : BaseRequestHandler, IRequestHandler<GetAppSettingsQuery, GetAppSetting>
    {
        private readonly IAppSettingRepository _appSettingRepository;

        public GetAppSettingsHandler(
            IAppSettingRepository appSettingRepository,
            IMapper mapper) : base(mapper)
        {
            _appSettingRepository = appSettingRepository;
        }

        public async Task<GetAppSetting> Handle(GetAppSettingsQuery request,
            CancellationToken cancellationToken)
        {
            var settings = _appSettingRepository.GetReadOnlyQuery().Select(Mapper.Map<GetAppSetting>);

            return settings.FirstOrDefault(x => x.Name == request.Name);
        }
    }
}