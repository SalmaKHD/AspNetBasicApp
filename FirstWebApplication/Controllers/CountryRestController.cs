using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO;

namespace FirstWebApplication.Controllers
{
    public class CountryRestController : BaseCountryRestController
    {
        private ICountriesService _countriesService;

        public CountryRestController(ICountriesService countriesService)
        {
            _countriesService = countriesService;

            // bad practice, ok until db implementation is ready
            _countriesService.AddCountry(new CountryAddRequest
            {
                Name = "Brazil"
            });

            _countriesService.AddCountry(new CountryAddRequest
            {
                Name = "Canada"
            });
        }

        [HttpGet] // we may imply this by method name, but not mandatory -> GetCountry
        public async Task<ActionResult> Cities()
        {
            // automatially done when adding attribute constraints
            if(!ModelState.IsValid)
            {
                return ValidationProblem(ModelState); // for validation errors
            }

            var coutries = await _countriesService.GetCountries();
            if (coutries.IsNullOrEmpty())
            {
                // for reporting any kind of problems, can be used for any type of exceptions also
                return Problem(detail: "No countries found", statusCode: 404, title: "Cities");
            }
            return Ok(coutries);
        }
    }
}
