using Microsoft.AspNetCore.Identity;

namespace JWTWebApiAuth.Services
{
    /// <summary>
    /// Interface for JWT operations management
    /// </summary>
    public interface IJwtManagement
    {
        object GenerateJwtToken(IdentityUser user);
    }
}