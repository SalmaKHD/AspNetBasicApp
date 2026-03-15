using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts.DTO
{
    public class AuthenticationResponse
    {
        public string? PersonName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
        public string? Expiration { get; set; }
        public DateTime RefreshTokenExpirationDateTime { get; set; }

    }
}
