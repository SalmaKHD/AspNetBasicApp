using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryContracts
{
    public interface ICountriesRepository
    {
        Task<Country> AddCountry(Country country);
        Task<List<Country>> GetAllCountries();
        Task<bool> DeleteCountry(Guid id);
        Task<Country?> GetCountryByID(Guid countryID);
        Task<Country?> GetCountryByName(string name);
    }
}
