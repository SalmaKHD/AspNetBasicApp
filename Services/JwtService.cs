using Entities.IdentityEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using ServiceContracts;
using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                new Claim(ClaimTypes.Name, user.PersonName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
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
                Expiration = expiration.ToString(),
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpirationDateTime = DateTime.Now.AddMinutes(
                    Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
            };
        }

        public string GenerateRefreshToken()
        {
            Byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create(); // avoid duplication

            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);

        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
        {
            // extract data from token
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = true
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            // validate according to parameters
            ClaimsPrincipal principle = jwtSecurityTokenHandler.ValidateToken(token,
                tokenValidationParameters, out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase)

                )
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principle;
             // make sure token is valid
        }
    }
}
