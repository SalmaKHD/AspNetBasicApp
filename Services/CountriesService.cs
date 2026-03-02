using Entities;
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

        public CountriesService()
        {
            _countries = new List<Country>();
        }

        #region AddCountry
        public CountryResonse AddCountry(CountryAddRequest? request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(CountryAddRequest));
            }

            // check validation rules
            ValidationContext validationContext = new ValidationContext(request);
            var validationResults = new List<ValidationResult>();

            // validate DTO
            ValidationHelper.ValidateDto(request);

            Country country = request.toCountry();
            _countries.Add(country);

            return country.toCountryResponse();
        }
    }
    #endregion
}
