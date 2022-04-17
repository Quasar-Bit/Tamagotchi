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
        private readonly IAppSettingsRepository _appSettingsRepository;

        public GetAllSettingsHandler(
            IAppSettingsRepository appSettingsRepository,
            IMapper mapper) : base(mapper)
        {
            _appSettingsRepository = appSettingsRepository;
        }

        public async Task<IEnumerable<GetAppSetting>> Handle(GetAppSettingsQuery request,
            CancellationToken cancellationToken)
        {
            return _appSettingsRepository.GetReadOnlyQuery().Select(Mapper.Map<GetAppSetting>);
        }
    }
}