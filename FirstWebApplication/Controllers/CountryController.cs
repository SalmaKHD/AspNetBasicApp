using FirstWebApplication.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace FirstWebApplication.Controllers
{
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private ICountriesService _countriesService;

        public CountryController(ICountriesService countriesService)
        {
            this._countriesService = countriesService;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Countries()
        {
            var countries = _countriesService.GetCountries();

            return View("Country", countries);

        }

        [Route("[action]")] // /"route" overrides [Route("[controller]")]
        [HttpPost]
        [TypeFilter(typeof(AddCountryActionFilter))]
        public IActionResult Add(CountryAddRequest country)
        {
            if (ModelState.IsValid)
            {
                _countriesService.AddCountry(country);
            } else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v =>
                v.Errors).Select(e => e.ErrorMessage).ToList();
            }
                var countries = _countriesService.GetCountries();
            return View("Country", countries);
        }

        [Route("[action]")] // /"route" overrides [Route("[controller]")]
        [HttpPost]
        public IActionResult Delete(CountryDeleteRequest country)
        {
            if (ModelState.IsValid)
            {
                _countriesService.DeleteCountry(country);
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v =>
                v.Errors).Select(e => e.ErrorMessage).ToList();
            }
            var countries = _countriesService.GetCountries();
            return View("Country", countries);
        }
    }
}
