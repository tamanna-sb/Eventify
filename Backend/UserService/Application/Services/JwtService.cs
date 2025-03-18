using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Eventify.Backend.UserService.Infrastructure.Entities;
using Eventify.Backend.UserService.Application.Interfaces;

namespace Eventify.Backend.UserService.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };


            // Read the Secret Key, Issuer, and Audience from appsettings.json
            var secretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("Jwt:SecretKey", "Secret key cannot be null or empty.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)); // Secret key from appsettings.json
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

             public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["Jwt:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("Jwt:SecretKey", "Secret key cannot be null or empty.");
            }
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = key
            };

            try
            {
                // Try to validate the token and parse it
                tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                // Check if the token is a valid JwtSecurityToken
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    // Optionally, check expiration here (TokenValidationParameters already validates lifetime)
                    if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
                    {
                        return false;
                    }
                }

                return true; // Token is valid
            }
            catch (Exception)
            {
                return false; // Token is invalid or an error occurred
            }
        }

    }
}
