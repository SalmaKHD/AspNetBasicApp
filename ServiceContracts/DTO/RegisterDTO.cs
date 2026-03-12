using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceContracts.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="name cannot be empty")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "email cannot be empty")]
        [EmailAddress(ErrorMessage ="email format incorrect")]
        public string Email { get; set; }

        [Required(ErrorMessage = "phone cannot be empty")]
        [RegularExpression("^[0-9]*$", ErrorMessage ="Phone number format incorrect")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "password cannot be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "confirm password cannot be empty")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
