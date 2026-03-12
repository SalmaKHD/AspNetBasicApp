using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceContracts.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "email cannot be empty")]
        [EmailAddress(ErrorMessage = "email format incorrect")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "password cannot be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
