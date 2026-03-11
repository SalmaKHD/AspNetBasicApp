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
        private readonly Mock<ILogger<CountriesService>> _mockLogger;

        public CountriesServiceTest(ITestOutputHelper testOutputHelper)
        {
            // initialize fixture
            _fixture = new Fixture();

            // initialize repository
            _countriesRespoMock = new Mock<ICountriesRepository>();
            _countriesRepository = _countriesRespoMock.Object;

            // initialize logger
            _mockLogger = new Mock<ILogger<CountriesService>>();
            _mockLogger.Setup(logger =>
           logger.Log(
               It.IsAny<LogLevel>(),
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception>(),
               It.IsAny<Func<It.IsAnyType, Exception?, string>>()
           )
       ).Verifiable();

            // initialize service
            _countryService = new CountriesService(_countriesRepository, _mockLogger.Object);

            // initialize test helper
            _testOutputHelper = testOutputHelper;

            // old way for fake testing with dbcontext directly -> no repo implementation
            // add EntityFrameworkCoreMock.Moq and test again (for Fake testing)
            //var countriesInitialData = new List<Country>() { };
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
            // initilaize mock data
            var request = _fixture.Build<CountryAddRequest>()
                .With(temp => temp.Name, "Brazil")
                .Create<CountryAddRequest>();

            var country = request.toCountry();

            // initialize mock repo method
            _countriesRespoMock.Setup(temp => temp.AddCountry(It.IsAny<Country>()))
            .ReturnsAsync(country);

            var addResponse = await _countryService.AddCountry(request);
            var expected_response = country.toCountryResponse();

            // add log to test results
            _testOutputHelper.WriteLine(request.ToString());

            // move to fluent api
            addResponse.CountryName.Should().Be(expected_response.CountryName);
        }
    }
}