using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Tamagotchi.Application.Localization.Interface;

namespace Tamagotchi.Application.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private static Dictionary<string, string> _localizations = null;
        private static string _currentLanguage = null;

        public LocalizedString this[string name]
        {
            get
            {
                if (name == null) throw new ArgumentNullException(nameof(name));

                if (_localizations == null) InitLanguage(@"Localization\en.txt");

                var translation = _localizations[name];

                return new LocalizedString(name, translation ?? name, translation == null);
            }
        }

        private static Dictionary<string, string> Init()
        {
            var path = Path.GetFullPath(_currentLanguage);
            
            path = path.Replace(".Web", ".Application");
            var result = new Dictionary<string, string>();

            var fileLines = File.ReadAllLines(path);

            foreach (var item in fileLines)
            {
                var arr = item.Split(" = ");
                result.Add(arr[0], arr[1]);
            }

            return result;
        }

        public void InitLanguage(string language)
        {
            _currentLanguage = language;
            _localizations = Init();
        }

        public LocalizedString this[string name, params object[] arguments] => throw new NotImplementedException();

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
    }
}