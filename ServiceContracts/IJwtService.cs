using Entities.IdentityEntities;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}
