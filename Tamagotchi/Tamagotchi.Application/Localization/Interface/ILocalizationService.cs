
using Microsoft.Extensions.Localization;

namespace Tamagotchi.Application.Localization.Interface
{
    public interface ILocalizationService : IStringLocalizer
    {
        void InitLanguage(string language);
    }
}