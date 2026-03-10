using CsvHelper;
using CsvHelper.Configuration;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private CountriesDbContext _db;
        private ILogger<CountriesService> _logger;

        public CountriesService(CountriesDbContext countriesDbContext, ILogger<CountriesService> logger)
        {
            _db = countriesDbContext;
            _logger = logger;
        }

        public async Task<List<CountryResonse>> GetCountries()
        {
            // no need to call SaveChanges()
            // get all eagerly first then execute Select on resultant list
            return await _db.Coutries.Select(country => country.toCountryResponse()).ToListAsync();
        }

        #region AddCountry
        public async Task<CountryResonse> AddCountry(CountryAddRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(CountryAddRequest));
            }

            // validate DTO
            ValidationHelper.ValidateDto(request);

            Country country = request.toCountry();
            _db.Coutries.Add(country);
            await _db.SaveChangesAsync();

            return country.toCountryResponse();
        }

        public async Task<bool> DeleteCountry(CountryDeleteRequest? request)
        {
            // add log
            _logger.LogInformation($"Deleting country with id {request.CountryID}");

            if (request == null)
            {
                throw new ArgumentNullException(nameof(CountryDeleteRequest));
            }

            // validate DTO
            ValidationHelper.ValidateDto(request);
            if (request.CountryID == null) return false;
            _db.Coutries.Remove(_db.Coutries.First(country => country.CountryID == request.CountryID));
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<MemoryStream> GetCountriesCsv()
        {
            var countries = await GetCountries();
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            CsvWriter csvWriter = new CsvWriter(writer, csvConfiguration);

            // write content

            // write custom fields
            csvWriter.WriteField(nameof(CountryResonse.CountryName));
            csvWriter.NextRecord();
            csvWriter.WriteField("Salma");
            csvWriter.NextRecord();

            csvWriter.WriteHeader<CountryResonse>();
            csvWriter.NextRecord();
            await csvWriter.WriteRecordsAsync(countries);
            csvWriter.NextRecord();
            csvWriter.Flush(); // to add to memory stream

            stream.Position = 0;
            return stream;

            // possible to write only some fields 


        }
    }
    #endregion
}
