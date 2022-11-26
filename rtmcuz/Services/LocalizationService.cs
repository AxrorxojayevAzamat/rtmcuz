using Microsoft.Extensions.Localization;
using System.Reflection;
using rtmcuz.Resources.Controllers;

namespace rtmcuz.Services
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationService(IStringLocalizerFactory factory)
        {
            var assemblyName = new AssemblyName(typeof(HomeController).GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("HomeController", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }
    }
}
