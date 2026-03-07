using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstWebApplication.Filters.ActionFilters
{
    public class GlobalActionFilter : IActionFilter, IOrderedFilter
    {
        private readonly ILogger<GlobalActionFilter> _logger;
        private readonly string _key;
        private readonly string _value;
        public int Order { get; set; }

        public GlobalActionFilter(ILogger<GlobalActionFilter> logger, string key, string value, int order)
        {
            _logger = logger;
            _key = key;
            _value = value;
            Order = order;
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
