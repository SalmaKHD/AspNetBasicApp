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
using OfficeOpenXml;
using RepositoryContracts;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private ICountriesRepository _countriesReository;
        private ILogger<CountriesService> _logger;

        public CountriesService(ICountriesRepository countryRepository, ILogger<CountriesService> logger)
        {
            _countriesReository = countryRepository;
            _logger = logger;
        }

        public async Task<List<CountryResonse>> GetCountries()
        {
            // no need to call SaveChanges()
            // get all eagerly first then execute Select on resultant list
            return (await _countriesReository.GetAllCountries()).Select(country => country.toCountryResponse()).ToList();
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
            await _countriesReository.AddCountry(country);

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
            await _countriesReository.DeleteCountry(request.CountryID ?? Guid.Empty);
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

            csvWriter.WriteHeader<CountryResonse>();
            csvWriter.NextRecord();
            await csvWriter.WriteRecordsAsync(countries);
            csvWriter.NextRecord();
            csvWriter.Flush(); // to add to memory stream

            stream.Position = 0;
            return stream;

            // possible to write only some fields 
        }

        public async Task<MemoryStream> GetCountriesExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            // to create excel content
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                // add a worksheet in workbook
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.Add("CountriesSheet");

                workSheet.Cells["A1"].Value = "Country Name";

                var countries = await GetCountries();
                int currentRow = 2;
                foreach (CountryResonse country in countries)
                {
                    // row number, column number
                    workSheet.Cells[currentRow++, 1].Value = country.CountryName;
                }

                workSheet.Cells[$"A1:H{currentRow}"].AutoFitColumns(); // adjust size based on content

                await excelPackage.SaveAsync();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
    #endregion
}
