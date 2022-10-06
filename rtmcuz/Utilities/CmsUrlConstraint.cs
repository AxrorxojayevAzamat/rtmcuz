using rtmcuz.Data;

namespace rtmcuz.Utilities
{
    public class CmsUrlConstraint : IRouteConstraint
    {
        private readonly RtmcUzContext _context;

        public bool Match(HttpContext? httpContext, IRouter? route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {

            var pages = _context.Pages;
            if (values[parameterName] != null)
            {
                var permalink = values[parameterName].ToString();
                return pages.Any(p => p.Permalink == permalink);
            }
            return false;
        }
    }
}
