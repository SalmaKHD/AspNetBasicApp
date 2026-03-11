using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Pkcs;
using System.Text;

namespace Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        // no validatons in repo
        private readonly ApplicationDbContext _db;

        public CountriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Country> AddCountry(Country country)
        {
            _db.Coutries.Add(country);
            await _db.SaveChangesAsync();

            return country;
        }

        public async Task<bool> DeleteCountry(Guid id)
        {
            var country = await _db.Coutries.FirstOrDefaultAsync(c => c.CountryID == id);
            if (country != null)
            {
                _db.Coutries.Remove(country);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _db.Coutries.ToListAsync();
        }

        public async Task<Country?> GetCountryByID(Guid countryID)
        {
            return await _db.Coutries.FirstOrDefaultAsync(c => countryID == c.CountryID);
        }

        public async Task<Country?> GetCountryByName(string name)
        {
            return await _db.Coutries.FirstOrDefaultAsync(c => name == c.Name);
        }
    }
}
