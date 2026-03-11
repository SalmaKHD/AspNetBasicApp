using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class Country
    {
        [Key]
        [Required]
        public Guid CountryID { get; set; }


        [StringLength(40)] // add constraint
        // nullable column
        public string? Name { get; set; }

        public Country(string? name)
        {
            CountryID = Guid.NewGuid();
            Name = name;
        }

        public Country(string? name, Guid guid)
        {
            CountryID = guid;
            Name = name;
        }

        public override bool Equals(object? obj)
        {
            // safeguard type casting and null comparison
            if (obj is not Country other)
                return false;

            // equality should rely on unique identifier (CountryID)
            // optionally also compare Name if you'd like semantic equality
            return CountryID.Equals(other.CountryID) &&
                   string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public ICollection<Person>? Persons; // will return persons with this country, Include function required
    }
}
