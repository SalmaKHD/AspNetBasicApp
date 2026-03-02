using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countryService;

        public CountriesServiceTest(ICountriesService countriesService)
        {
            _countryService = countriesService;
        }

        [Fact]
        public void AddCountry_NullCountry()
        {
            CountryAddRequest? request = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _countryService.AddCountry(request);
            });
        }
    }
}
