using Microsoft.AspNetCore.Identity;

namespace JWTWebApiAuth.Services
{
    public interface IJwtManagement
    {
        object GenerateJwtToken(string email, IdentityUser user);
    }
}