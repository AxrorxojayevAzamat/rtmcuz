using Microsoft.Extensions.Localization;
using System.Reflection;
using rtmcuz.Resources;

namespace rtmcuz.Services
{
    public class CultureService
    {
        private readonly IStringLocalizer _localizer;

        public CultureService(IStringLocalizerFactory factory)
        {

        }

        public void SetCulture(string key)
        {
            // RequestCulture requestCulture = new(culture);
            // Response.Cookies.Append(
            //     CookieRequestCultureProvider.DefaultCookieName,
            //     CookieRequestCultureProvider.MakeCookieValue(requestCulture),
            //     new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        }
    }
}
