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
    /// <summary>
    /// Class used to handle operations on JWT on user side
    /// </summary>
    public class JwtManagement : IJwtManagement
    {
        private readonly JWTSettings _jwtSettings;

        public JwtManagement(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }


        /// <summary>
        /// Method generates JWT authentication token. It's used to authenticate user in API.
        /// </summary>
        /// <param name="user">Signed in user data</param>
        /// <returns>The JWT Security Token</returns>
        public object GenerateJwtToken(IdentityUser user)
        {
            // list of registered claims that we want to use
            var registeredClaims  = new []
            {
                new Claim (ClaimTypes.Name, user.Email)  
            };

            // security key generated based on the secret key stored in appsettings.json file
            var securityKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            // generates singing credentials to verify the JWT sign
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // build JWT security token
            var secuirtyToken = new JwtSecurityToken(

                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: registeredClaims,
                expires: DateTime.Now.AddMinutes(Int32.Parse(_jwtSettings.ExpirationTime)),
                signingCredentials: signingCredentials   
                
            );

            // additional data in payload
            secuirtyToken.Payload["DateGenerated"] = DateTime.Now;
            secuirtyToken.Payload["Some other data"] = "Test data";

            // generate the JWT token
            return new JwtSecurityTokenHandler().WriteToken(secuirtyToken);
        }
    }
}