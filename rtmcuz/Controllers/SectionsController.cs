using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace rtmcuz.Controllers
{
    public class SectionsController : Controller
    {

        [Route("dashboard")]
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }
    }
}
