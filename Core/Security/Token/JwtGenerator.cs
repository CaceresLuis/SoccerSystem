using System;
using System.Text;
using Infrastructure.Models;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Core.Security.Token
{
    public class JwtGenerator : IJwtGenerator
    {
        //Get global configurations
        private readonly IConfiguration _configuration;

        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(UserEntity user, List<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            if (roles != null)
            {
                foreach (string rol in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }
            string keySecret = _configuration["SecretKey"];
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keySecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
