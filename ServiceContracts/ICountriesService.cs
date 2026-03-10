using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts
{
    public interface ICountriesService
    {
        Task<CountryResonse> AddCountry(CountryAddRequest? response);
        Task<bool> DeleteCountry(CountryDeleteRequest? request);
        Task<List<CountryResonse>> GetCountries();
    }
}
