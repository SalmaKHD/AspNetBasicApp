using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
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
        public CountryResonse AddCountry(CountryAddRequest? response)
        {
            if(response == null)
            {
                throw new ArgumentNullException(nameof(CountryAddRequest));
            }

            if (response.Name == null)
            {
                throw new ArgumentException(nameof(response.Name));
            }

            Country country = response.toCountry();
            _countries.Add(country);

            return country.toCountryResponse();
        }
    }
    #endregion
}
