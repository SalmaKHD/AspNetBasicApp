using Entities.IdentityEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class JwtService : IJwtService
    {
        public readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationResponse CreateJwtToken(ApplicationUser user)
        {
            // create expiration date
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));
            // create payload
            Claim[] claims = new Claim[]
            {
                // first 3: essential, must be present
                // payload data
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                // additional details
                new Claim(ClaimTypes.NameIdentifier, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.PersonName ?? "")
            };

            // create secret key
            // for hashing and generating signature section
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // create signaure section
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials);

            // generate token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponse()
            {   
                Token = token,
                Email = user.Email,
                PersonName = user.PersonName,
                Expiration = expiration.ToString()
            };
        }
    }
}
