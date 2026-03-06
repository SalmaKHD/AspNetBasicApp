using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstWebApplication.Filters.ActionFilters
{
    public class CountryControllerActionFilter : IAsyncActionFilter
    {

        private readonly ILogger<CountryControllerActionFilter> _logger;
        private readonly string _key;
        private readonly string _value;

        public CountryControllerActionFilter(ILogger<CountryControllerActionFilter> logger, string key, string value)
        {
            _logger = logger;
            _key = key;
            _value = value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // onActionExecuting logic
            await next();
            // onActionExecuted logic
            // add header
            context.HttpContext.Response.Headers[_key] = _value;
        }
    }
}
