using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Application.Base;
using Tamagotchi.Application.Settings.Base.DTOs;
using Tamagotchi.Application.Settings.Commands.Update.DTOs;
using Tamagotchi.Data.Repositories.Interfaces;

namespace Tamagotchi.Application.Settings.Commands.Update
{
    internal class UpdateAppSettingsHandler : BaseRequestHandler, IRequestHandler<UpdateAppSettingsCommand, GetAppSetting>
    {
        private readonly IAppSettingRepository _appSettingRepository;

        public UpdateAppSettingsHandler(
            IAppSettingRepository appSettingRepository,
            IMapper mapper) : base(mapper)
        {
            _appSettingRepository = appSettingRepository;
        }

        public async Task<GetAppSetting> Handle(UpdateAppSettingsCommand request,
            CancellationToken cancellationToken)
        {
            var existingAppSettings = await _appSettingRepository.GetChangeTrackingQuery().FirstOrDefaultAsync(x => x.Id == request.Id, new CancellationToken());

            if (existingAppSettings == null)
                return null;

            var result = _appSettingRepository.Update(request.Adapt(existingAppSettings));

            await _appSettingRepository.UnitOfWork.SaveChangesAsync(new CancellationToken());

            return Mapper.Map<GetAppSetting>(result);
        }
    }
}