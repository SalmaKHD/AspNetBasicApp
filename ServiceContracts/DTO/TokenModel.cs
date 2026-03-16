using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.DTO
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
