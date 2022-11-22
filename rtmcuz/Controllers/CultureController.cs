﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace rtmcuz.Controllers
{
    public class CultureController : Controller
    {
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            RequestCulture requestCulture = new(culture);
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(requestCulture),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return LocalRedirect(returnUrl);
        }
    }
}