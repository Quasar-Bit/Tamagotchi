
using MediatR;
using Tamagotchi.Application.Settings.Base.DTOs;

namespace Tamagotchi.Application.Settings.Commands.Update.DTOs
{
    public class UpdateAppSettingsCommand : GetAppSetting, IRequest<GetAppSetting>
    {
    }
}