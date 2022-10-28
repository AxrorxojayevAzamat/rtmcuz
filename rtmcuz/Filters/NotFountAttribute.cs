using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using rtmcuz.Data;

namespace rtmcuz.Filters
{
    public class NotFountAttribute : Attribute, IAsyncActionFilter
    {
        private readonly RtmcUzContext _context;
        public NotFountAttribute(RtmcUzContext context)
        {
            _context = context;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //int id = (int)context.ActionArguments[key: "id"];
            //if (id == null || _context.Sections == null)
            //{
            //    return NotFound();
            //}

            //var section = await _context.Sections.FindAsync(id);
            //if (section == null)
            //{
            //    return;
            //}
            //await next();
        }
    }
}
