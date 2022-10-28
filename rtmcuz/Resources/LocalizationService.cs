using Microsoft.Extensions.Localization;
using System.Reflection;

namespace rtmcuz.Resources
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationService(IStringLocalizerFactory factory)
        {
            var assemblyName = new AssemblyName(typeof(ApplicationResource).GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("ApplicationResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }
    }
}
