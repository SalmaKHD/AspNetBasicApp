using Entities;
using FirstWebApplication.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Runtime.InteropServices;

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

        [Route("pdf")]
        public async Task<IActionResult> CountriesPDF()
        {
            // Get a list of countries
            var countries = await _countriesService.GetCountries();
            return new ViewAsPdf("Country", countries, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Top = 20,
                    Bottom = 20,
                    Left = 20,
                    Right = 20
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }

        [Route("csv")]
       public async Task<IActionResult> CountriesCSV()
        {
            var memoryStream = await _countriesService.GetCountriesCsv();
            return File(memoryStream, "application/octet-stream", "countries.csv");
        }
    }
}
