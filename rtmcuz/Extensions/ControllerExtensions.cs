using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;

namespace rtmcuz.Extensions
{
    /// <summary>
    /// Controller extensions
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Test if a view exists
        /// </summary>
        public static bool ViewExists(this ControllerBase controller, string name)
        {
            var services = controller.HttpContext.RequestServices;
            var viewEngine = services.GetRequiredService<ICompositeViewEngine>();
            var result = viewEngine.GetView(null, name, true);
            if (!result.Success)
                result = viewEngine.FindView(controller.ControllerContext, name, true);
            return result.Success;
        }
    }
}
