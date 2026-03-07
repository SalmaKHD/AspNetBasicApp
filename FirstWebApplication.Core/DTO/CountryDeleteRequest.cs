using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceContracts.DTO
{
    public class CountryDeleteRequest
    {
        // add validation atribute to DTO object
        [Required(ErrorMessage = "Name of country cannot be empty.")]
        public Guid? CountryID { get; set; }
    }
}
