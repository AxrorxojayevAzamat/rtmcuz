using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace rtmcuz.Controllers
{
    [Route("dashboard/{sectionType}/{action}")]
    public class DashboardController : Controller
    {
        public string _sectionType;
        public DashboardController(string sectionType)
        {
            _sectionType = sectionType;
        }



        //[Route("dashboard")]
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            _ = _sectionType;
            return View();
        }
    }
}
