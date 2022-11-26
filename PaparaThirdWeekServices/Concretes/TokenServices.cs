using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PaparaThirdWeek.Services.Concretes
{
  public class TokenServices : ITokenServices
    {
        private readonly IConfiguration configuration;

        public TokenServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            {"user1","password1" },
            {"admin","123" }
        };

        public TokenDto Authenticate(UserDto user)
        {
            if (!UserRecords.Any(x => x.Key == user.Name && x.Value == user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.Now.AddMinutes(configuration.GetValue<int>("Expires")),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenDto
            {
                Token = tokenHandler.WriteToken(token)
            };
        }



    }
}


