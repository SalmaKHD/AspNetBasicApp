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

namespace Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countryService;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ILogger<CountriesService> _logger;

        public CountriesServiceTest(ITestOutputHelper testOutputHelper, ILogger<CountriesService> logger)
        {
            var countriesInitialData = new List<Country>() { };
            // add EntityFrameworkCoreMock.Moq and test again
            //DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
            //    new DbContextOptionsBuilder<ApplicationDbContext>().Options
            //    );

            //ApplicationDbContext dbContext = dbContextMock.Object;

            //dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);

            //_countryService = new CountriesService(dbContext, logger);
            //_testOutputHelper = testOutputHelper;
        }

        // for every possible input, create one test to ensure correct functionality
        [Fact]
        public async Task AddCountry_NullCountry()
        {
            CountryAddRequest? request = null;

            // add log to test results
            _testOutputHelper.WriteLine(request.ToString());

            // async method
            await Assert.ThrowsAsync<ArgumentNullException>(async () => // async lambda
            {
                // execute async function
                await _countryService.AddCountry(request);
            });
        }


        [Fact]
        public async Task AddCountry_CorrectCountry()
        {
            var request = new CountryAddRequest
            {
                Name = "Brazil"
            };

            var addResponse = await _countryService.AddCountry(request);

            // add log to test results
            _testOutputHelper.WriteLine(request.ToString());

            Assert.True(addResponse.CountryName == request.Name);
        }
    }
}
