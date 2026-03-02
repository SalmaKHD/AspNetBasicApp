using Entities;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ServiceContracts.DTO
{
    public class CountryResonse
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}

public static class CountryExensions
{
    public static CountryResonse toCountryResponse(this Country country)
    {
        return new CountryResonse()
        {
            CountryId = country.CountryID,
            CountryName = country.Name
        };
    }
}