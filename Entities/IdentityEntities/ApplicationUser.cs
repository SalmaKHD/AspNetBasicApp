using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.IdentityEntities
{
    // ApplicationUser: convension
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
        // for verifying user
        public string? Refresh { get; set; }
        public DateTime RefreshTokenExpirationDateTime { get; set; }
    }
}
