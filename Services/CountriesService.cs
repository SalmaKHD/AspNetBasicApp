using Entities;
using Exceptions;
using Microsoft.Extensions.Logging;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private List<Country> _countries;
        private ILogger<CountriesService> _logger;

        public CountriesService(ILogger<CountriesService> logger)
        {
            _countries = new List<Country>();
            _logger = logger;
        }

        #region AddCountry
        public CountryResonse AddCountry(CountryAddRequest? request)
        {
            throw new CustomInvalidArgumentException();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(CountryAddRequest));
            }

            // validate DTO
            ValidationHelper.ValidateDto(request);

            Country country = request.toCountry();
            _countries.Add(country);

            return country.toCountryResponse();
        }

        public bool DeleteCountry(CountryDeleteRequest? request)
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
            int isDeleted = _countries.RemoveAll(coutry => coutry.CountryID == request?.CountryID);
            return isDeleted != -1;
        }


        public List<CountryResonse> GetCountries()
        {
            return _countries.Select(country => country.toCountryResponse()).ToList();

            ;
        }
    }
    #endregion
}
