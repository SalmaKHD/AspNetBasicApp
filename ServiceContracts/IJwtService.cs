using Entities.IdentityEntities;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
        // for extracting user payload
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
