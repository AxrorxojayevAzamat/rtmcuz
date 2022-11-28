using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace rtmcuz.Controllers
{
    public class CultureController : Controller
    {
        [Route("SetLanguage")]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            SetCookie(culture);
            return LocalRedirect(returnUrl);
        }

        public void SetCookie(string culture)
        {
            RequestCulture requestCulture = new(culture);
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(requestCulture),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        }
    }
}