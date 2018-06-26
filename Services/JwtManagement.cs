using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTWebApiAuth.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTWebApiAuth.Services
{
    public class JwtManagement : IJwtManagement
    {
        private readonly JWTSettings _jwtSettings;

        public JwtManagement(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public object GenerateJwtToken(string email, IdentityUser user)
        {
            var registeredClaims  = new []
            {
                new Claim (ClaimTypes.Name, user.Email)  
            };

            var securityKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secuirtyToken = new JwtSecurityToken(

                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: registeredClaims,
                expires: DateTime.Now.AddMinutes(Int32.Parse(_jwtSettings.ExpirationTime)),
                signingCredentials: signingCredentials   
                
            );

            secuirtyToken.Payload["DateGenerated"] = DateTime.Now;
            secuirtyToken.Payload["Some other data"] = "Test data";

            return new JwtSecurityTokenHandler().WriteToken(secuirtyToken);
        }
    }
}