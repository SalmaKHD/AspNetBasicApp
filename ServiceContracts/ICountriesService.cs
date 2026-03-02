using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts
{
    public interface ICountriesService
    {
        CountryResonse AddCountry(CountryAddRequest? response);
    }
}
