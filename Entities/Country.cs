using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class Country
    {
        [Key]
        public Guid CountryID { get; set; }


        [StringLength(40)] // add constraint
        // nullable column
        public string? Name { get; set; }

        public Country(string? name)
        {
            CountryID = Guid.NewGuid();
            Name = name;
        }
    }
}
