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
        private readonly ITestOutputHelper _testOutputHelper;

        public CountriesServiceTest(ICountriesService countriesService, ITestOutputHelper testOutputHelper)
        {
            _countryService = countriesService;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void AddCountry_NullCountry()
        {
            CountryAddRequest? request = null;

            // add log to test results
            _testOutputHelper.WriteLine(request.ToString());

            Assert.Throws<ArgumentNullException>(() =>
            {
                _countryService.AddCountry(request);
            });
        }
    }
}
