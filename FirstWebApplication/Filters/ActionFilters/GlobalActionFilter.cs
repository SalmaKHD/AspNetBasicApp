using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstWebApplication.Filters.ActionFilters
{
    public class GlobalActionFilter : IActionFilter
    {
        private readonly ILogger<GlobalActionFilter> _logger;
        private readonly string _key;
        private readonly string _value;

        public GlobalActionFilter(ILogger<GlobalActionFilter> logger, string key, string value)
        {
            _logger = logger;
            _key = key;
            _value = value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // add header
            context.HttpContext.Response.Headers[_key] = _value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
