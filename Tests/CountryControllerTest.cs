using AutoFixture;
using FirstWebApplication.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class CountryControllerTest
    {
        private readonly Mock<ICountriesService> _mockCountryService;
        private readonly ICountriesService _countriesService;

        private readonly Fixture _fixture;

        public CountryControllerTest()
        {
            _fixture = new Fixture();

            _mockCountryService = new Mock<ICountriesService>();
            _countriesService = _mockCountryService.Object;
        }

        [Fact]
        public async Task CountriesView_ShouldReturnCountriesList()
        {
            List<CountryResonse> response_list = _fixture.Create<List<CountryResonse>>();

            CountryController countryController = new CountryController(_countriesService);

            // methods that are called in the controller should be mocked
            _mockCountryService
                .Setup(temp => temp.GetCountries())
                .ReturnsAsync(response_list);

            IActionResult result = await countryController.Countries();

            // check component type
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            // check model data type
            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<CountryResonse>>();
            // check model
            viewResult.ViewData.Model.Should().Be(response_list);
        }
    }
}
