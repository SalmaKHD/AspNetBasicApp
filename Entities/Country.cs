using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Country
    {
        public Guid CountryID { get; set; }
        public string? Name { get; set; }

        public Country(string? name)
        {
            CountryID = Guid.NewGuid();
            Name = name;
        }
    }
}
