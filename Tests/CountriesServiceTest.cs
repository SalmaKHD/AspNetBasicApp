//using ServiceContracts;
//using ServiceContracts.DTO;
//using System;
//using System.Collections.Generic;
//using System.Text;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Abstractions;
using Moq;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using AutoFixture;
using FluentAssertions;
using RepositoryContracts;

namespace Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ILogger<CountriesService> _logger;
        private readonly IFixture _fixture;
        private readonly Mock<ICountriesRepository> _countriesRespoMock; // mock repository
        private readonly ICountriesRepository _countriesRepository;

        public CountriesServiceTest(ITestOutputHelper testOutputHelper, ILogger<CountriesService> logger)
        {
            _fixture = new Fixture();

            _countriesRespoMock = new Mock<ICountriesRepository>();
            _countriesRepository = _countriesRespoMock.Object;

            var countriesInitialData = new List<Country>() { };

            _countryService = new CountriesService(_countriesRepository);
            _testOutputHelper = testOutputHelper;

            // add EntityFrameworkCoreMock.Moq and test again (for Fake testing)
            //DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
            //    new DbContextOptionsBuilder<ApplicationDbContext>().Options
            //    );
            //ApplicationDbContext dbContext = dbContextMock.Object;
            //dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);
        }

        // for every possible input, create one test to ensure correct functionality
        [Fact]
        public async Task AddCountry_NullCountry_ArgumentNullException()
        {
            // mock return value of the target method
            _countriesRespoMock.Setup(temp => temp.AddCountry(It.IsAny<Country>()))
                .ReturnsAsync(new Country("Brazil"));

            CountryAddRequest? request = null;

            // add log to test results
            _testOutputHelper.WriteLine(request.ToString());

            // async method, for Assert method only
            //await Assert.ThrowsAsync<ArgumentNullException>(async () => // async lambda
            //{
            //    // execute async function
            //    await _countryService.AddCountry(request);
            //});

            Func<Task> action = async () =>
            {
                await _countryService.AddCountry(request);
            };

            await action.Should().ThrowAsync<ArgumentNullException>();
        }


        [Fact]
        public async Task AddCountry_CorrectCountry_Successful()
        {
            // only a single method shuld be tested

            // use Build if you need to customize property values
            var request = _fixture.Build<CountryAddRequest>()
                .With(temp => temp.Name, "Brazil")
                .Create<CountryAddRequest>();

            var country = request.toCountry();

            _countriesRespoMock.Setup(temp => temp.AddCountry(It.IsAny<Country>()))
            .ReturnsAsync(country);

            var addResponse = await _countryService.AddCountry(request);
            var expected_response = country.toCountryResponse();

            // add log to test results
            _testOutputHelper.WriteLine(request.ToString());

            // move to fluent api
            addResponse.Should().Be(expected_response);
        }
    }
}