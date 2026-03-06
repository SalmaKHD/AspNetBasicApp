using Entities;
using FirstWebApplication.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;

namespace FirstWebApplication.Filters.ActionFilters
{
    public class AddCountryActionFilter : IActionFilter
    {
        private readonly ILogger<AddCountryActionFilter> _logger;
        private readonly string _key;
        private readonly string _value;

        public AddCountryActionFilter(ILogger<AddCountryActionFilter> logger, string key, string value)
        {
            _logger = logger;
            _key = key;
            _value = value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Countries action executed");
            // receive action arguments and pass to view

            IDictionary<string, object?>? parameters = (IDictionary<string, object?>?)context.HttpContext.Items["arguments"];
            CountryController countryController = (CountryController)context.Controller;

            if(parameters != null)
            {
                if(parameters.ContainsKey("country"))
                {
                    var country = (CountryAddRequest?)parameters["country"];
                    // we can set to view data, will be available in view
                    countryController.ViewData["country"] = country;
                    _logger.LogInformation($"Value of country action argument: {country?.Name}");
                }
            }

            // add as response header
            context.HttpContext.Response.Headers[_key] = _value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // pass action arguments to action executed method
            context.HttpContext.Items["arguments"] = context.ActionArguments;

            _logger.LogInformation("Countries action executing");

            // read arequest arguments
            if(context.ActionArguments.ContainsKey("country"))
            {
                var country = (CountryAddRequest?) context.ActionArguments["country"];
                _logger.LogInformation($"Value of country action argument: {country?.Name}");
            }
        }
    }
}
