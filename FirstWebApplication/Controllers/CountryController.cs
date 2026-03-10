using FirstWebApplication.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace FirstWebApplication.Controllers
{
    [Route("[controller]")]
    [TypeFilter(typeof(CountryControllerActionFilter), Arguments = new object[] {
            "Controller-Filter-Key",
            "value"},
        Order = 2
        )]
    public class CountryController : Controller
    {
        private ICountriesService _countriesService;

        public CountryController(ICountriesService countriesService)
        {
            this._countriesService = countriesService;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Countries()
        {
            var countries = await _countriesService.GetCountries();

            return View("Country", countries);

        }

        [Route("[action]")] // /"route" overrides [Route("[controller]")]
        [HttpPost]
        [TypeFilter(typeof(AddCountryActionFilter), Arguments = new object[] {
            "Action-Filter-Key",
            "value"},
            Order = 1
        )]
        public async Task<IActionResult> Add(CountryAddRequest country)
        {
            if (ModelState.IsValid)
            {
               await _countriesService.AddCountry(country);
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v =>
                v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            var countries = await _countriesService.GetCountries();
            return View("Country", countries);
        }

        [Route("[action]")] // /"route" overrides [Route("[controller]")]
        [HttpPost]
        public async Task<IActionResult> Delete(CountryDeleteRequest country)
        {
            if (ModelState.IsValid)
            {
                await _countriesService.DeleteCountry(country);
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v =>
                v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            var countries = await _countriesService.GetCountries();
            return View("Country", countries);
        }
    }
}
