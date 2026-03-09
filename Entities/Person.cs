using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Person
    {

        [Key]
        [Required]
        public Guid PersonID { get; set; }

        public Guid? CountryID { get; set; }

        [StringLength(40)]
        public string? Name { get; set; }

        [ForeignKey("CountryID")] 
        public Country? Country { get; set; } // will be returned with Include function only
    }
}
