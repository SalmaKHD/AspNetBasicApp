using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceContracts.DTO
{
    public class CountryAddRequest
    {
        public string? Name { get; set; }

        public Country toCountry()
        {
            return new Country()
            {
                Name = Name
            };
        }
    }
}
